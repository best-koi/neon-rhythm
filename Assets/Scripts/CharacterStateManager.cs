using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


enum AnimState
{
    LEFT, DOWN, UP, RIGHT, HORIZONTAL, VERTICAL
}
public class CharacterStateManager : MonoBehaviour
{

    private AnimState characterState;
    public Sprite none;
    public Sprite left;
    public Sprite down;
    public Sprite up;
    public Sprite right;
    public Sprite horizontal;
    public Sprite vertical;

    // Start is called before the first frame update
    void Start()
    {
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
                this.GetComponent<Image>().sprite = left;
                break;
            case AnimState.DOWN:
                this.GetComponent<Image>().sprite = down;
                break;
            case AnimState.UP:
                this.GetComponent<Image>().sprite = up;
                break;
            case AnimState.RIGHT:
                this.GetComponent<Image>().sprite = right;
                break;
            case AnimState.HORIZONTAL:
                this.GetComponent<Image>().sprite = horizontal;
                break;
            case AnimState.VERTICAL:
                this.GetComponent<Image>().sprite = vertical;
                break;
            default:
                this.GetComponent<Image>().sprite = none;
                break;
        }
    }

    void DState() { characterState = AnimState.LEFT; }
    void FState() { characterState = AnimState.DOWN; }
    void JState() { characterState = AnimState.UP; }
    void KState() { characterState = AnimState.RIGHT; }
}
