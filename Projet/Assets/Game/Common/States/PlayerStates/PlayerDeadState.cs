using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : State<PlayerController>
{
    private static PlayerDeadState instance = new PlayerDeadState();

    public static PlayerDeadState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.StopVelocity();
    }

    public override void Execute(PlayerController player)
    {
    }

    public override void ExitState(PlayerController player)
    {
    }
}
