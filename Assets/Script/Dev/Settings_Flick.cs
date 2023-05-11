using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings_Flick : MonoBehaviour
{
    //This is coordinated with GameSettings by hand.
    /*
     * 0 : Flick Threshold
     * 1 : Shake Threshold
     * 2 : 
     */
    
    public GameObject[] sliders;
    public TMP_Text[] texts;

    private void Start ()
    {
        sliders [0].GetComponent<Slider> ().value = GameSettings.flickThreshold;
        sliders [1].GetComponent<Slider> ().value = GameSettings.shakeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach ( GameObject slider in sliders )
        {
            texts [i].text = slider.GetComponent<Slider>().value.ToString("F1");
            i++;
        }

        GameSettings.flickThreshold = sliders [0].GetComponent<Slider> ().value;
        GameSettings.shakeThreshold = sliders [1].GetComponent<Slider> ().value;
    }
}
