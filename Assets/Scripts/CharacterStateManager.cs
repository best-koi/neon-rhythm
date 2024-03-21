using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


enum AnimState
{
    LEFT, DOWN, UP, RIGHT, HORIZONTAL, VERTICAL
}
public class CharacterStateManager : MonoBehaviour
{

    private AnimState characterState;
    public int characterSelect = 0;
    public Sprite[] nones;
    public Sprite[] lefts;
    public Sprite[] downs;
    public Sprite[] ups;
    public Sprite[] rights;
    public Sprite[] horizontals;
    public Sprite[] verticals;

    // Start is called before the first frame update
    void Start()
    {
        characterSelect = SlideshowController.charVal;
        this.GetComponent<Image>().sprite = nones[characterSelect];

        InputController.onDInput += DState;
        InputController.onFInput += FState;
        InputController.onJInput += JState;
        InputController.onKInput += KState;
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitcher(characterState);
    }

    void StateSwitcher(AnimState animState)
    {
        switch (animState)
        {
            case AnimState.LEFT:
                this.GetComponent<Image>().sprite = lefts[characterSelect];
                break;
            case AnimState.DOWN:
                this.GetComponent<Image>().sprite = downs[characterSelect];
                break;
            case AnimState.UP:
                this.GetComponent<Image>().sprite = ups[characterSelect];
                break;
            case AnimState.RIGHT:
                this.GetComponent<Image>().sprite = rights[characterSelect];
                break;
            case AnimState.HORIZONTAL:
                this.GetComponent<Image>().sprite = horizontals[characterSelect];
                break;
            case AnimState.VERTICAL:
                this.GetComponent<Image>().sprite = verticals[characterSelect];
                break;
            default:
                this.GetComponent<Image>().sprite = nones[characterSelect];
                break;
        }
    }

    void DState() { characterState = AnimState.LEFT; }
    void FState() { characterState = AnimState.DOWN; }
    void JState() { characterState = AnimState.UP; }
    void KState() { characterState = AnimState.RIGHT; }
}
