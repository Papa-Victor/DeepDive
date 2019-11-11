using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpPlayerInputState : State<PlayerController>
{
    protected PumpPlayerInputState() {}

    protected static PumpPlayerInputState instance = new PumpPlayerInputState();

    private RepairPumpStation station;

    public static PumpPlayerInputState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.StopVelocity();
        station = (RepairPumpStation)player.GetComponent<PlayerStationHandler>().GetHighlightStation();
    }

    public override void Execute(PlayerController player)
    {
        if(Input.GetButtonDown("PumpItUp") && station.AddPress()){
            player.ChangeState(RegularPlayerInputState.GetInstance());
        }
    }

    public override void ExitState(PlayerController player)
    {
        station = null;
    }
}
