using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //Required for Math.Abs

public enum ArrowType
{
    D_LEFT, F_DOWN, J_UP, K_RIGHT
}

public class JSONRead : MonoBehaviour
{
    //This Score and Slider implementation is temporary and simply used to check if the score and health bar are functioning properly
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText; //Text for displaying int of notes hit in a row
    public Slider healthBar;

    private float playerScore; //This value stores the player score
    private int noteCombo;
    private float playerHealth; //This value stores the player health, current implementation is starting with 10 health, losing 1 per mistake, but gaining 0.25 per successful hit
    private int noteIndex = 0; //This is part of the note spawning implementation

    private float currentNoteTime = 0; //This is part of the note spawning implementation and records how much time has passed for the current note

    //Boolean values to record whether a note is being "held"
    private float DHold = -1;
    private float FHold = -1;
    private float JHold = -1;
    private float KHold = -1;
    //The following variables are used to store Lists for the current notes on screen in a specific category, 
    //only the time values will be stored for each of the parts of these Lists, and Lists are being used but only the value at index 0 is being considered so that
    //we won't have any issues regarding multiple notes of the same category being on screen at once and overwriting their expected time values; this will also prevent us from traversing the main track array multiple times
    //When a note is played it will be removed from this List, but this is only done if it is done at the right time. A note value will also be removed if a note is note played and the time reaches into the negatives.
    //~~~~~~~~FOR CLARIFICATION: My current assumption is that if travel time is 5 seconds, .2 seconds is the ideal time to hit the note, and 0 seconds will constitude a failure. This would mean that the mp3 would be played 4.8 seconds into the game rather than 5
    public List<NoteTime> dTimes;
    public List<NoteTime> fTimes;
    public List<NoteTime> jTimes;
    public List<NoteTime> kTimes;

    public static Action<ArrowType> onSpawnNote; //Action used to tell other scripts that a note should be spawned.

    public static Action<ArrowType, Accuracy> onPlayNote; //Action used to tell other scripts that a note has been played

    public float noteSpeedFactor; // These values are used to determine how long it will take for notes to reach the bottom of the screen from when they spawn in; calculated in Start()
    //These three leeway values could be calculated in Start() based upon songInfo bpm
    public float successTimeLeeway = 0.1f; //This is the value that determines how long the note will keep traveling after it is expected to be hit //PERFECT
    public float greatTimeLeeway = 0.17f; //GREAT
    public float goodTimeLeeway = 0.22f; //GOOD
    [SerializeField] public TextAsset textJSON;

    [System.Serializable]
    public class Note //Adjust this depending on what values each "note" actually has in the JSON file
    {
        public string name; //functions as note track
        public int midi;
        public float time; //Time since start of song
        public float velocity;
        public float duration;
    }

    [System.Serializable]
    public class Track // for "tracks": section
    {
        public float startTime;
        public float duration;
        public int length;
        public Note[] notes;
    }

    [System.Serializable]
    public class TimeSignature // for the "timeSignature": section
    {
        public float absoluteTime;
        public float seconds;
        public int numerator;
        public int denominator;
        public int click;
        public int notesQ;
    }

    [System.Serializable] //"tempo": section
    public class Tempo
    {
        public float absoluteTime;
        public float seconds;
        public float bpm;
    }

    [System.Serializable] //"header": section
    public class Header 
    {
        public int PPQ;
        public int[] timeSignature;
        public float bpm;
        public string name;
    }

    public class SongInfo //overarching section in json file
    {
        public Header header;
        public Tempo[] tempo;
        public TimeSignature[] timeSignature;
        public float startTime;
        public float duration;
        public Track[] tracks;
    }

    public class NoteTime //data structure element to record a note's spawn time and duration
    {
        public float time;
        public float length;
    }

    public SongInfo songInfo = new SongInfo(); //Defines a track to be filled
    public Note currentNote; //Part of note spawning implementation
    // Start is called before the first frame update
    public Note[] songNotes; //creates reference array for songs

    private bool AddingNotes; //Check for whether the notes track has been finished
    void Start()
    {
        AddingNotes = true; //Instantiates track seeking as valid
        //Establishes health and score values
        playerScore = 0;
        playerHealth = 50; //This currently means that 125 notes can be missed

        //Establishes Action connections
        InputController.onDInput += DInputReception;
        InputController.onFInput += FInputReception;
        InputController.onJInput += JInputReception;
        InputController.onKInput += KInputReception;
        InputController.onDRelease += DReleaseReception;
        InputController.onFRelease += FReleaseReception;
        InputController.onJRelease += JReleaseReception;
        InputController.onKRelease += KReleaseReception;

        //Establishes Lists for note time storage
        dTimes = new List<NoteTime>();
        fTimes = new List<NoteTime>();
        jTimes = new List<NoteTime>();
        kTimes = new List<NoteTime>();

        songInfo = JsonUtility.FromJson<SongInfo>(textJSON.text); //Possibly make the name of the json file serializable
        songNotes = songInfo.tracks[0].notes;

        noteSpeedFactor = 5; //Current assumption is that notes will take 5 seconds to travel, this could be replaced with a calculation using songInfo.tracks[0].length which is an int and songInfo.tracks[0].duration which is a float for time
    }

    // Update is called based on a fixed time, this function is used to spawn in notes based upon inputs from json file
    void FixedUpdate()
    {
        currentNote = songNotes[noteIndex];
        if (currentNote.time <= currentNoteTime) //Spawns in note when previous note is finished (depending on implementation for json files, maybe impact this by a multiplication or division factor)
        {

            if (AddingNotes) SpawnNote(currentNote); //Only spawns a note when there are more notes to spawn
            if (noteIndex < (songNotes.Length - 1))
            {
                noteIndex++; //increments array if possible
                currentNote = songNotes[noteIndex]; //Updates current note
            }
            else
            {
                AddingNotes = false; //Tells the loop to fuck off
                if (currentNoteTime > (songInfo.tracks[0].duration + (2 * noteSpeedFactor))) //Ends the level if there are no more notes to generate, after the time of the song has ended, plus double the animation travel time, so that the final notes have time to be played
                {
                    EndLevel();
                }
            }
        }

        //This portion of code is used to clear the zero elements from the note time Lists when they have passed the zero time threshold
        //Times.Count > 0 is included to make sure that a value in index 0 exists before trying to run the conditional check
        if (dTimes.Count > 0 && dTimes[0].time < currentNoteTime)
        {
            dTimes.RemoveAt(0);
            Debug.Log($"D dead {currentNoteTime}");
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health because a note was missed
            onPlayNote?.Invoke(ArrowType.D_LEFT, Accuracy.MISS); //D note deletion called for miss
        }
        if (fTimes.Count > 0 && fTimes[0].time < currentNoteTime)
        {
            fTimes.RemoveAt(0);
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health because a note was missed
            onPlayNote?.Invoke(ArrowType.F_DOWN, Accuracy.MISS); //F note deletion called for miss
        }
        if (jTimes.Count > 0 && jTimes[0].time < currentNoteTime)
        {
            jTimes.RemoveAt(0);
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health because a note was missed
            onPlayNote?.Invoke(ArrowType.J_UP, Accuracy.MISS); //J note deletion called for miss
        }
        if (kTimes.Count > 0 && kTimes[0].time < currentNoteTime)
        {
            kTimes.RemoveAt(0);
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health because a note was missed
            onPlayNote?.Invoke(ArrowType.K_RIGHT, Accuracy.MISS); //K note deletion called for miss
        }

        healthBar.value = playerHealth/50f; //Sets visible value on healthbar to be a percentage assuming total health is 10HP
        scoreText.SetText("Score: " + playerScore); //Updates score value in text element each frame
        comboText.SetText("Combo: " + noteCombo); //Updates combo value in text element each frame

        currentNoteTime += Time.deltaTime; //Increments time

        DHold -= Time.deltaTime;
        FHold -= Time.deltaTime;
        JHold -= Time.deltaTime;
        KHold -= Time.deltaTime;

        //Adds score for notes currently being held
        //These loops are all separated, since if multiple holds are active, the score application should compound
        if (DHold>0)
        {
            AdjustScore(50 * Time.deltaTime, false); //Adds 50 score per second - not comboing as this is a part of the holding function
        }
        if (FHold>0)
        {
            AdjustScore(50 * Time.deltaTime, false); //Adds 50 score per second - not comboing as this is a part of the holding function
        }
        if (JHold>0)
        {
            AdjustScore(50 * Time.deltaTime, false); //Adds 50 score per second - not comboing as this is a part of the holding function
        }
        if (KHold>0)
        {
            AdjustScore(50 * Time.deltaTime, false); //Adds 50 score per second - not comboing as this is a part of the holding function
        }
    }

    void SpawnNote(Note note) //current assumption is that octave 4 F A C and E will be used for D F J and K respectively
    {
        if (note.name == "F4")//Implementation for spawning D notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.D_LEFT);
            Debug.Log($"D Shot {currentNoteTime}");
            NoteTime newTimeD = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = 0 //Length is set to zero when it is not a hold note
            };
            dTimes.Add(newTimeD); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "A4")//Implementation for spawning F notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.F_DOWN);
            NoteTime newTimeF = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = 0 //Length is set to zero when it is not a hold note
            };
            fTimes.Add(newTimeF); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "C4")//Implementation for spawning J notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.J_UP);
            NoteTime newTimeJ = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = 0 //Length is set to zero when it is not a hold note
            };
            jTimes.Add(newTimeJ); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "E4")//Implementation for spawning K notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.K_RIGHT);
            NoteTime newTimeK = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = 0 //Length is set to zero when it is not a hold note
            };
            kTimes.Add(newTimeK); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }  

        //FOR THESE FOUR ITERATIONS, MAKE A MODIFICATION OF ONSPAWNNOTE IN ORDER TO ALLOW FOR HOLD NOTE SPAWNING, IN TERMS OF VISUALS
        //Hold Notes - That have a # in them, meaning length will be set to the duration value for the note
        else if (note.name == "F#4")//Implementation for spawning D notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.D_LEFT);
            Debug.Log($"D Shot {currentNoteTime}");
            NoteTime newTimeD = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = note.duration
            };
            dTimes.Add(newTimeD); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "A#4")//Implementation for spawning F notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.F_DOWN);
            NoteTime newTimeF = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = note.duration
            };
            fTimes.Add(newTimeF); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "C#4")//Implementation for spawning J notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.J_UP);
            NoteTime newTimeJ = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = note.duration
            };
            jTimes.Add(newTimeJ); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
        else if (note.name == "E#4")//Implementation for spawning K notes goes here
        {
            onSpawnNote?.Invoke(ArrowType.K_RIGHT);
            NoteTime newTimeK = new NoteTime
            {
                time = note.time + (noteSpeedFactor - successTimeLeeway),
                length = note.duration
            };
            kTimes.Add(newTimeK); //Considers time for this note being hit to be at the note spawning time, plus the animation time, subtracting the leeway for the note to continue past the goalpost
        }
    }

    //All of the following input reception functions are used to analyze the accuracy of the input, they will check for perfect first, then great, then good, then for complete misses; the first three of these will result in index 0 of the list being removed
    // and modify the score and health values accordingly
    //This function is run when D button is pressed
    void DInputReception()
    {
        if (dTimes.Count > 0) //Makes sure that the respective note has atleast one element of itself on the screen
        {
            Accuracy passedAcc; 
            float accuracy = Math.Abs(dTimes[0].time - currentNoteTime); //This value records how much time the user was away from hitting the note perfectly on time
            passedAcc = RateNote(accuracy);
            if (passedAcc != Accuracy.MISS) //The note is rated and score/health values are properly adjusted, but if the note was found to be hit then it is removed from the time List
            {
                onPlayNote?.Invoke(ArrowType.D_LEFT, passedAcc); //D note has been played
                if (dTimes[0].length == 0) //Not a hold note
                {
                    //dTimes.RemoveAt(0);
                    //onPlayNote?.Invoke(ArrowType.D_LEFT, passedAcc); //D note has been played
                }
                else
                {
                    DHold = dTimes[0].length; //Activates holding mechanic for the given note
                }
                dTimes.RemoveAt(0);
            }
        }
        else //Considers wrong note input otherwise
        {
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health for incorrect input
        }
    }

    //This function is run when F button is pressed
    void FInputReception()
    {
        if (fTimes.Count > 0) //Makes sure that the respective note has atleast one element of itself on the screen
        {
            Accuracy passedAcc;
            float accuracy = Math.Abs(fTimes[0].time - currentNoteTime); //This value records how much time the user was away from hitting the note perfectly on time
            Debug.Log("D Recep");
            passedAcc = RateNote(accuracy);
            if (passedAcc != Accuracy.MISS) //The note is rated and score/health values are properly adjusted, but if the note was found to be hit then it is removed from the time List
            {
                onPlayNote?.Invoke(ArrowType.F_DOWN, passedAcc); //D note has been played
                if (fTimes[0].length == 0) //Not a hold note
                {
                    //fTimes.RemoveAt(0);
                    //onPlayNote?.Invoke(ArrowType.F_DOWN, passedAcc); //D note has been played
                }
                else
                {
                    FHold = fTimes[0].length; //Activates holding mechanic for the given note
                }
                fTimes.RemoveAt(0);
            }
        }
        else //Considers wrong note input otherwise
        {
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health for incorrect input
        }
    }

    //This function is run when J button is pressed
    void JInputReception()
    {
        if (jTimes.Count > 0) //Makes sure that the respective note has atleast one element of itself on the screen
        {
            Accuracy passedAcc;
            float accuracy = Math.Abs(jTimes[0].time - currentNoteTime); //This value records how much time the user was away from hitting the note perfectly on time
            Debug.Log("D Recep");
            passedAcc = RateNote(accuracy);
            if (passedAcc != Accuracy.MISS) //The note is rated and score/health values are properly adjusted, but if the note was found to be hit then it is removed from the time List
            {
                onPlayNote?.Invoke(ArrowType.J_UP, passedAcc); //D note has been played
                if (jTimes[0].length == 0) //Not a hold note
                {
                    //jTimes.RemoveAt(0);
                    //onPlayNote?.Invoke(ArrowType.J_UP, passedAcc); //D note has been played
                }
                else
                {
                    JHold = jTimes[0].length; //Activates holding mechanic for the given note
                }
                jTimes.RemoveAt(0);
            }
        }
        else //Considers wrong note input otherwise
        {
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health for incorrect input
        }
    }

    //This function is run when K button is pressed
    void KInputReception()
    {
        if (kTimes.Count > 0) //Makes sure that the respective note has atleast one element of itself on the screen
        {
            Accuracy passedAcc;
            float accuracy = Math.Abs(kTimes[0].time - currentNoteTime); //This value records how much time the user was away from hitting the note perfectly on time
            Debug.Log("D Recep");
            passedAcc = RateNote(accuracy);
            if (passedAcc != Accuracy.MISS) //The note is rated and score/health values are properly adjusted, but if the note was found to be hit then it is removed from the time List
            {
                onPlayNote?.Invoke(ArrowType.K_RIGHT, passedAcc); //D note has been played
                if (kTimes[0].length == 0) //Not a hold note
                {
                    //kTimes.RemoveAt(0);
                    //onPlayNote?.Invoke(ArrowType.K_RIGHT, passedAcc); //D note has been played
                }
                else
                {
                    KHold = kTimes[0].length; //Activates holding mechanic for the given note
                }
                kTimes.RemoveAt(0);
            }
        }
        else //Considers wrong note input otherwise
        {
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health for incorrect input
        }
    }

    //Functions for release reception in order to calculate hold times for hold notes:
    //These functions all clear hold if it is currently active, and also perform a delete if it was active
    void DReleaseReception()
    {
        if (DHold>0)
        {
            //dTimes.RemoveAt(0);
            //onPlayNote?.Invoke(ArrowType.D_LEFT, Accuracy.PERFECT); //D note hold has ended - possibly add a rate function here to compare to the maximum possible hold time
            DHold = -1;
        }
        Debug.Log("D Released");
    }

    void FReleaseReception()
    {
        if (FHold>0)
        {
            //fTimes.RemoveAt(0);
            //onPlayNote?.Invoke(ArrowType.F_DOWN, Accuracy.PERFECT); //F note hold has ended - possibly add a rate function here to compare to the maximum possible hold time
            FHold = -1;
        }
    }

    void JReleaseReception()
    {
        if (JHold>-1)
        {
            //jTimes.RemoveAt(0);
            //onPlayNote?.Invoke(ArrowType.J_UP, Accuracy.PERFECT); //J note hold has ended - possibly add a rate function here to compare to the maximum possible hold time
            JHold = 0;
        }
    }

    void KReleaseReception()
    {
        if (KHold>-1)
        {
            //kTimes.RemoveAt(0);
            //onPlayNote?.Invoke(ArrowType.K_RIGHT, Accuracy.PERFECT); //K note hold has ended - possibly add a rate function here to compare to the maximum possible hold time
            KHold = 0;
        }
    }

    //This function is called within each of the InputReception functions in order to rate a note input based upon accuracy
    //It returns a bool if the note was "hit" at all, which will then signal for that note to be removed from the list in the respection input reception function
    Accuracy RateNote(float accuracy)
    {
        if (accuracy < successTimeLeeway) //Perfect note placement
        {
            AdjustScore(200f, true); //Gives 200 score for a perfect note
            AdjustHealth(2f); //Gives the player 0.25 HP for a successful note placement
            return Accuracy.PERFECT; //Note was hit
        }
        else if (accuracy < greatTimeLeeway) //Great note placement
        {
            AdjustScore(100f, true); //Gives 100 score for a great note
            AdjustHealth(2f); //Gives the player 0.25 HP for a successful note placement
            return Accuracy.GREAT; //Note was hit
        }
        else if (accuracy < goodTimeLeeway) //Good note placement
        {
            AdjustScore(50f, true); //Gives 50 score for a good note
            AdjustHealth(2f); //Gives the player 0.25 HP for a successful note placement
            return Accuracy.GOOD; //Note was hit
        }
        else //Early note press
        {
            AdjustScore(0f, true); //Sends signal that a note was missed, potentially breaking a combo
            AdjustHealth(-1f); //Removes one health for hitting a note early
            Debug.Log("Early Input");
        }
        return Accuracy.MISS;
    }

    //Adjusts the score by the given changeValue
    //This function will also update the score demonstration in the UI
    void AdjustScore(float changeValue, bool Comboing) //Only modifies combo if boolean is true, in order to allow for usage of this function along side hold notes
    {
        if (changeValue > 0 && Comboing)
        {
            noteCombo++; //Determines how many notes have been hit in a row
        }
        else if (Comboing) //When changeValue == 0
        {
            noteCombo = 0; //Resets combo
        }
        playerScore += changeValue * (noteCombo / 2 + 1); //Multiplies score gain in a format of x1, x2, x3 etc for every 4 notes, currently with no limit
    }

    //Adjusts the health by the given changeValue
    //This function will also update 
    void AdjustHealth(float changeValue)
    {
        playerHealth += changeValue;
        if (playerHealth < 0)
        {
            EndLevel();
        }
    }

    //This function is called when either the level has been completed or failed
    void EndLevel()
    {
        Debug.Log("The level should end here"); //Add implementation for level ending
    }
}