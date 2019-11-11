using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairEvent", menuName = "Events/RepairEvent")]
public class RepairEvent : sEvent
{
    public enum RepairEventType { Hatch, Pipe, Pump}

    public float fullDPS;
    public float reducedDPS;
    public float timeToRepair;

    public RepairEventType type;

    private EventManager evMng;

    private RepairStation rStation;
    public RepairStation station
    {
        get { return rStation; }
        set { rStation = value; }
    }

    private bool init = false;

    private float timeRemaining;
    private float currentTickTime;
    private float tickDamageDelay;


    private void Awake()
    {
        fullDPS = 2.5f;
        reducedDPS = 1.0f;
        timeToRepair = 5.0f;
    }

    override public void InitiateEvent(EventManager eM)
    {
        evMng = eM;
        rStation.AttachEvent(this);
        timeRemaining = timeToRepair;
        ApplyDamage();

        currentTickTime = 0f;
        tickDamageDelay = 0.5f;

        init = true;   
    }

    override public void Interacting(bool result = true)
    {
        //EventResolved(true);
    }

    override public void EventResolved(bool result = true)
    {
        RepairStation();
        rStation.DetachEvent();
        evMng.UnsubscribeEvent(this);
        //Destroy(gameObject);
    }


    override public void Tick(float deltaTime)
    {
        if (!init)
            return;

        switch (rStation.State)
        {
            case RepairState.NewBroken:
                timeRemaining -= deltaTime;
                if (timeRemaining < 0)
                {
                    RepairDelayOut();
                }
                break;

            case RepairState.OldBroken:
                currentTickTime += deltaTime;
                if(currentTickTime >= tickDamageDelay)
                {
                    currentTickTime = 0;
                    ApplyTickDamage();
                }
                break;
        }
    }

    public void RepairStation()
    {
        switch (station.State)
        {
            case RepairState.NewBroken:
                FullRepair();
                break;
            case RepairState.OldBroken:
                HalfRepair();
                break;
        }        
    }


    private void ApplyDamage()
    {
        evMng.map.AddDamage(fullDPS, fullDPS);
        rStation.State = RepairState.NewBroken;
    }

    private void ApplyTickDamage()
    {
        evMng.map.AddDamage(reducedDPS, 0);
    }

    private void RepairDelayOut()
    {
        evMng.map.RemoveDamage(0, fullDPS - reducedDPS);
        rStation.State = RepairState.OldBroken;
    }

    private void FullRepair()
    {
        evMng.map.RemoveDamage(fullDPS, fullDPS);
        rStation.State = RepairState.Normal;
    }

    private void HalfRepair()
    {
        evMng.map.RemoveDamage(fullDPS - reducedDPS, fullDPS - reducedDPS);
        rStation.State = RepairState.Normal;
    }

    public RepairEventType GetRepairType() { return type; }
}
