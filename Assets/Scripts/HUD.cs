using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public HUDResources ResourcesBar;
    public HUDToolbox ToolBox;


    public GameObject TaskProgressBarObject;
    public Image TaskProgressBarFilling;
    public Text TaskProgressBarText;


    void Start()
    {
        TaskProgressBarObject.SetActive(false);
    }


    public void TaskProgressBar(float quantity, string text)
    {
        TaskProgressBarObject.SetActive(true);
        TaskProgressBarFilling.fillAmount = quantity;
        TaskProgressBarText.text = text;
    }
}
