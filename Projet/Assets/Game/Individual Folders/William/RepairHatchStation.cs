using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairHatchStation : RepairStation
{
    public override void Interact(PlayerController pc)
     {
        if (HasActiveEvent())
        {
            pc.ChangeState(HatchPlayerInputState.GetInstance());
            Audio_Cues[2].Play();
            GetEvent().Interacting(true);

            TooltipPrompter.Instance.SetPressX(false);
            TooltipPrompter.Instance.SetRotateLTrigger(true);
        }
    }

    public bool SendCompletedCircles(int completedCircles)
    {
        RepairHatchEvent rhe = GetEvent() as RepairHatchEvent;
        bool temp = rhe.ReceiveCompletedCircles(completedCircles);

        if (temp)
        {
            Anim.SetBool("Active", false);
            Audio_Cues[1].Pause();
            Audio_Cues[2].Pause();
            Audio_Cues[3].Play();

            TooltipPrompter.Instance.SetRotateLTrigger(false);
            return true;
        }
        return false;
    }
}
