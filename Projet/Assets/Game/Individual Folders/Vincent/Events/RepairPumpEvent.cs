using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairPumpEvent", menuName = "Events/RepairPumpEvent")]
public class RepairPumpEvent : RepairEvent
{
    private int currentPresses;
    public int pressesToRepair;

    private void Awake()
    {
        currentPresses = 0;
        pressesToRepair = 10;
        timeToRepair = 10.0f;
        fullDPS = 1.5f;
        reducedDPS = 1.0f;
        type = RepairEventType.Pump;
    }

    public bool AddPress()
    {
        currentPresses++;
        if(currentPresses >= pressesToRepair)
        {
            EventResolved(true);
            return true;
        }
        return false;
    }
}
