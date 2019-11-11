using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RepairPipeEvent", menuName = "Events/RepairPipeEvent")]
public class RepairPipeEvent : RepairEvent
{
    private int currentThighs;
    private bool readyToTighten;
    public int thighsToRepair;

    private void Awake()
    {
        readyToTighten = true;
        currentThighs = 0;
        thighsToRepair = 5;
        timeToRepair = 5.0f;
        fullDPS = 3.0f;
        reducedDPS = 1.0f;
        type = RepairEventType.Pipe;
    }

    public bool Tighten()
    {
        if (readyToTighten)
        {
            currentThighs++;
            readyToTighten = false;
            if(currentThighs >= thighsToRepair)
            {
                EventResolved(true);
                return true;
            }
        }
        return false;
    }

    public void Release()
    {
        readyToTighten = true;
    }
}
