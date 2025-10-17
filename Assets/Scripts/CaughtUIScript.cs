using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaughtUIScript : MonoBehaviour
{   public Slider caughtSlider;
    public Image flashOverlay;
    public TextMeshProUGUI caughtText;

    public float caughtIncreaseRate = 10f;
    public float caughtDecreaseRate = 10f;
    public float maxCaughtValue = 100f;
    public bool playerVisible = false;

    public int flashCounter = 5;
    public float flashDuration = 0.5f;
    private bool isFlashing = false;


    // Start is called before the first frame update
    public void Start()
    {
        flashOverlay.enabled = false;
        caughtText.enabled = false;
        caughtSlider.value = 0;
    }

    // Update is called once per frame
    public void Update()
    {

        // Update caught slider based on player visibility
        if (playerVisible)
        {
            caughtSlider.value += caughtIncreaseRate * Time.deltaTime;

        }
        else
        {
            caughtSlider.value -= caughtDecreaseRate * Time.deltaTime;
        }
        caughtSlider.value = Mathf.Clamp(caughtSlider.value, 0, maxCaughtValue);

        // Show caught text if slider is above 100
        if (caughtSlider.value >= maxCaughtValue && !isFlashing)
        {
            StartCoroutine(FlashScreen());
        }

        if (flashCounter > 5)
        {
            caughtText.enabled = true;
            caughtText.text = "You're caught";
        }
        else
        {
            caughtText.enabled = false;
        }
        IncreasedDetection(0f);
    }
    public void IncreasedDetection(float amount)
    {
        caughtSlider.value += amount* Time.deltaTime;
        caughtSlider.value = Mathf.Clamp(caughtSlider.value, 0, maxCaughtValue);
    }
    IEnumerator FlashScreen()
    {
        //Flash the screen red multiple times when caught 
        isFlashing = true;
        flashOverlay.enabled = true;
        caughtText.enabled = true;
        caughtText.text = "You're caught";
        // Flash red and transparent alternately
        for (int i = 0; i < flashCounter; i++)
        {
            flashOverlay.color = new Color(1, 0, 0, 0.5f); // Red with half transparency
            yield return new WaitForSeconds(flashDuration);
            flashOverlay.color = new Color(1, 0, 0, 0); // Fully transparent
            yield return new WaitForSeconds(flashDuration);
        }
        
        flashOverlay.enabled = false;
        isFlashing = false;

    }
}
