using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingDoorPlayerState : State<PlayerController>
{
    protected UsingDoorPlayerState() { }

    protected static UsingDoorPlayerState instance = new UsingDoorPlayerState();

    protected static Door currentDoor;

    private static PlayerController player;

    public static UsingDoorPlayerState GetInstance(Door door)
    {
        currentDoor = door;
        return instance;
    }

    public override void EnterState(PlayerController player)
    {
        player.SuspendActivity();
        UsingDoorPlayerState.player = player;
        //Door door = (Door) player.GetComponent<PlayerStationHandler>().GetHighlightStation();
        currentDoor.Subscribe_Level_Loaded(LevelLoaded);
    }

    public override void Execute(PlayerController player)
    {
    }

    public override void ExitState(PlayerController player)
    {
        //PlayerStationHandler playerStationHandler = player.GetComponent<PlayerStationHandler>();
        //Door door = (Door)playerStationHandler.GetHighlightStation();
        currentDoor.Unsubscribe_Level_Loaded(LevelLoaded);
        UsingDoorPlayerState.player = null;
        currentDoor = null;
        player.ResumeActivity();
    }

    private void LevelLoaded()
    {
        player.ChangeState(RegularPlayerInputState.GetInstance());
    }
}
