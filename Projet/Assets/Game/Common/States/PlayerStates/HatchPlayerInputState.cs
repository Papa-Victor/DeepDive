using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchPlayerInputState : State<PlayerController>
{
    protected HatchPlayerInputState() {}

    protected static HatchPlayerInputState instance = new HatchPlayerInputState();
 
    private Joystick joystick;
    private RepairHatchStation station;

    public static HatchPlayerInputState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        joystick = new Joystick();
        player.StopVelocity();
        station = (RepairHatchStation) player.GetComponent<PlayerStationHandler>().GetHighlightStation();
    }

    public override void Execute(PlayerController player)
    {  
        Vector2 axises = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        joystick.UpdateAxises(axises);

        if (station.SendCompletedCircles(joystick.GetNbCircle()))
        {
            player.ChangeState(RegularPlayerInputState.GetInstance());
        }
    }

    public override void ExitState(PlayerController player)
    {
        joystick = null;
        station = null;
    }
    
    public Joystick GetJoystick()
    {
        return joystick;
    }
}
