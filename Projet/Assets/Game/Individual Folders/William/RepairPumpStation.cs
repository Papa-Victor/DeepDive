using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPumpStation : RepairStation
{
    public override void Interact(PlayerController pc)
     {
        if (HasActiveEvent())
        {
            pc.ChangeState(PumpPlayerInputState.GetInstance());
            Audio_Cues[2].Play();
            GetEvent().Interacting(true);

            TooltipPrompter.Instance.SetPressX(false);
            TooltipPrompter.Instance.SetSmashA(true);
        }
    }

    public bool AddPress()
    {
        bool temp = ((RepairPumpEvent)GetEvent()).AddPress();

        if(temp)
        {
            Anim.SetBool("Active", false);
            Audio_Cues[1].Pause();
            Audio_Cues[2].Pause();
            Audio_Cues[3].Play();

            TooltipPrompter.Instance.SetSmashA(false);
            
            return true;
        }
        return false;
    }
}
