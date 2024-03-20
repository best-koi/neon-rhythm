using UnityEngine;
using UnityEngine.UI;

public class SlideshowController : MonoBehaviour
{
    public Sprite[] slides;
    public Image slideImage;
    public Toggle[] toggles;

    private void Start()
    {
        // Set the initial slide
        SetSlide(0);
    }

    public void SetSlide(int index)
    {
        if (index >= 0 && index < slides.Length)
        {
            slideImage.sprite = slides[index];
            toggles[index].isOn = true;
        }
    }
}