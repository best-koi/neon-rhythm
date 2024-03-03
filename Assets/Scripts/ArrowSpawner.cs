using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum Accuracy
{
    MISS, GOOD, GREAT, PERFECT
}

public class ArrowSpawner : MonoBehaviour
{
    public ArrowType delegateArrow;
    public GameObject arrowToSpawn;
    private RectTransform rectTransform;
    private Vector3 spawnLocation;
    private List<GameObject> spawnedArrows;
    [SerializeField] private TMP_Text accuracyText;

    void Start()
    {
        spawnedArrows = new List<GameObject>();
        rectTransform = this.GetComponent<RectTransform>();
        spawnLocation = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
        JSONRead.onSpawnNote += SpawnArrowAbsolute;
        JSONRead.onPlayNote += DeleteArrowAbsolute;
    }

    void SpawnArrowAbsolute(ArrowType arrow)
    {
        if(arrow == delegateArrow)
        {
            GameObject spawnedArrow = Instantiate(arrowToSpawn, rectTransform);
            spawnedArrows.Add(spawnedArrow); //Adds spawnedArrow to the array of spawnedArrows of this type, for easier early deletion
            spawnedArrow.transform.SetParent(rectTransform, false);
            RectTransform spawnedArrowLTransform = spawnedArrow.GetComponent<RectTransform>();
            spawnedArrowLTransform.localPosition = new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z);
            /*if (spawnedArrow != null) //Alternatively implemented this deletion, by calling the DeleteArrowAbsolute invocation whenever a note is missed and score is reduced in "JSONRead.cs"
            {
                Destroy(spawnedArrow, 6);
            }*/
        }
    }

    void DeleteArrowAbsolute(ArrowType arrow, Accuracy acc)
    {
        if(arrow == delegateArrow && spawnedArrows.Count > 0) //Ensures that the proper arrow type is being considered, and that that arrow has arrows still spawned in
        {
            GameObject arrowToDelete = spawnedArrows[0]; //Records the current oldest arrow (so that if multiple arrows could be considered a hit, only the most accurate one is deleted)
            spawnedArrows.RemoveAt(0); //Removes recorded arrow from List
            switch(acc)
            {
                case Accuracy.PERFECT: accuracyText.text = "Perfect!"; break;
                case Accuracy.GREAT: accuracyText.text = "Great!"; break;
                case Accuracy.GOOD: accuracyText.text = "Good!"; break;
                default: accuracyText.text = "Miss!"; break;
            } //Requires an instance of arrow spawner to be present in the main scene
            Destroy(arrowToDelete); //Destroys most oldest child arrow
        }
    }
}
