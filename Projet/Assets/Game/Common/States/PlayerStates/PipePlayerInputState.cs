using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePlayerInputState : State<PlayerController>
{
    protected PipePlayerInputState() {}

    protected static PipePlayerInputState instance = new PipePlayerInputState();

    private RepairPipeStation station;

    public static PipePlayerInputState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.StopVelocity();
        station = (RepairPipeStation)player.GetComponent<PlayerStationHandler>().GetHighlightStation();
    }

    public override void Execute(PlayerController player)
    {

        float triggerInput = Input.GetAxis("ScrewYoMomma");
        if(triggerInput >= .9f)
        {
            if (station.Tighten()){
                player.ChangeState(RegularPlayerInputState.GetInstance());
            }
        }
        else if(triggerInput <= .1f)
        {
            station.Release();
        }
    }

    public override void ExitState(PlayerController player)
    {
        station = null;
    }
}
