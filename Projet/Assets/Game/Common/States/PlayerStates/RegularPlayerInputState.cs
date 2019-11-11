using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularPlayerInputState : State<PlayerController>
{
    protected RegularPlayerInputState() {}

    protected static RegularPlayerInputState instance = new RegularPlayerInputState();

    public static RegularPlayerInputState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
    }

    public override void Execute(PlayerController player)
    {
        if(player.GetInsideLadder() != null && Mathf.Abs(Input.GetAxis("Vertical")) > 0.7f)
        {
            player.ChangeState(LadderPlayerInputState.GetInstance());
            return;
        }
        if (Input.GetButton("Jump"))
        {
            player.Jump();
        }

        player.HorizontalMovement(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Interact"))
        {
            player.GetComponent<PlayerStationHandler>().Interact();
        }
    }

    public override void ExitState(PlayerController player)
    {
    }
}
