using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlayerInputState : State<PlayerController>
{
    protected LadderPlayerInputState() { }

    protected static LadderPlayerInputState instance = new LadderPlayerInputState();

    private AudioSource ladderAudio;

    public static LadderPlayerInputState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.GetOnLadder();
        ladderAudio = GameObject.FindGameObjectWithTag("Audio Ladder").GetComponent<AudioSource>();
        ladderAudio.Play();
    }

    public override void Execute(PlayerController player)
    {
        float verticalInput = Input.GetAxis("Vertical");
        player.MoveUpLadder(verticalInput);

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0.5f && player.CanGetOffLadder(horizontalInput))
        {
            player.ChangeState(RegularPlayerInputState.GetInstance());
        }
    }
    public override void ExitState(PlayerController player)
    {
        player.GetOffLadder();
        ladderAudio.Stop();
        ladderAudio = null;
    }
}
