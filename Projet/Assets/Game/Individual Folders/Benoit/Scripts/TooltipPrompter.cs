using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.DesignPattern;

public class TooltipPrompter : PublicSingleton<TooltipPrompter>
{
    public GameObject pressX;
    public GameObject pressRT;
    public GameObject rotateLTrigger;
    public GameObject smashA;

    public void SetPressX(bool value)
    {
        pressX.SetActive(value);
    }

    public void SetPressRT(bool value)
    {
        pressRT.SetActive(value);
        if(value)
            pressRT.GetComponent<PressRT>().Activate();
        else
            pressRT.GetComponent<PressRT>().Desactivate();
    }

    public void SetRotateLTrigger(bool value)
    {
        rotateLTrigger.SetActive(value);
    }

    public void SetSmashA(bool value)
    {
        smashA.SetActive(value);
    }
}
