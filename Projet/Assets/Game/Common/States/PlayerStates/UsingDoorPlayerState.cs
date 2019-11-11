using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingDoorPlayerState : State<PlayerController>
{
    protected UsingDoorPlayerState() { }

    protected static UsingDoorPlayerState instance = new UsingDoorPlayerState();

    private static PlayerController player;

    public static UsingDoorPlayerState GetInstance()
    {
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.SuspendActivity();
        UsingDoorPlayerState.player = player;
        Door door = (Door) player.GetComponent<PlayerStationHandler>().GetHighlightStation();
        door.Subscribe_Level_Loaded(LevelLoaded);
    }

    public override void Execute(PlayerController player)
    {
    }

    public override void ExitState(PlayerController player)
    {
        Door door = (Door)player.GetComponent<PlayerStationHandler>().GetHighlightStation();
        door.Unsubscribe_Level_Loaded(LevelLoaded);
        UsingDoorPlayerState.player = null;
        player.ResumeActivity();
    }

    private void LevelLoaded()
    {
        player.ChangeState(RegularPlayerInputState.GetInstance());
    }
}
