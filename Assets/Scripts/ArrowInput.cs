using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ArrowInput : MonoBehaviour
{
    public ArrowType typeArrow;
    private JSONRead inputJson;
    private RectTransform rectTransform;
    private float arrowSpeed;
    private float length = 1090;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        inputJson = FindObjectOfType<JSONRead>();
        arrowSpeed = inputJson.noteSpeedFactor;// - inputJson.goodTimeLeeway;
        rectTransform.localPosition = new Vector3(0, rectTransform.localPosition.y,0);
    }

    private void FixedUpdate()
    {
        rectTransform.localPosition = new Vector3(0, rectTransform.localPosition.y + (length * Time.fixedDeltaTime/arrowSpeed),0);
    }
}
