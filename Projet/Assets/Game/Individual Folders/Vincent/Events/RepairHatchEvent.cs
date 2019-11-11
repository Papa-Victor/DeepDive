using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairHatchEvent", menuName = "Events/RepairHatchEvent")]
public class RepairHatchEvent : RepairEvent
{
    private int completedCircles;
    public int circlesToRepair;

    private void Awake()
    {
        circlesToRepair = 3;
        timeToRepair = 20;
        fullDPS = 2.0f;
        reducedDPS = 1.0f;
        type = RepairEventType.Hatch;
    }

    public bool ReceiveCompletedCircles(int completedCircles)
    {
        if(completedCircles >= circlesToRepair)
        {
            EventResolved(true);
            return true;
        }
        return false;
    }
}
