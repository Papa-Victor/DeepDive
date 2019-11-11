using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC;
using CCC.Utility;

[System.Serializable]
public struct EventType
{
    public sEvent type;
    public float maxRate;
    public float minRate;
    public AnimationCurve distribution;
    public StationsGroup stations;
}

public class EventManager : MonoBehaviour
{
    public List<EventType> eventTypes;
    public RepairStation[] ScriptedStations;

    public float beginEventDelay;
    public float endEventDelayDelay;
    public float randomnessRatio = 0.5f;
    public AnimationCurve delaysGrowth;

    public Map map;

    private bool ScriptedDone = false;
    private int currentScriptedEventIndex = 0;
    [SerializeField]
    private float delayBetweenScripted;

    float depthRatio = 0;  

    [SerializeField]
    private List<sEvent> activeEvents = new List<sEvent>();

    [SerializeField, ReadOnly]
    private float eventDelay;
    private int count;

    private DialogueHandler diagH;

    private void Start()
    {
        map = Map.Instance;
        map.StopSpeed();

        NewEventDelay();
        diagH = GetComponent<DialogueHandler>();

        this.DelayedCall(() => { GenerateScriptedEvent(); }, delayBetweenScripted * 2);
    }

    private void Update()
    {
        eventDelay -= Time.deltaTime;

        depthRatio = (Map.Instance.depth / 1500.0f).Abs();

        foreach (sEvent ev in activeEvents)
            ev.Tick(Time.deltaTime);

        if (ScriptedDone && eventDelay <= 0)
        {
            NewEventDelay();
            GenerateEvent();
        }         
    }

    private void NewEventDelay()
    {
        float avTime = delaysGrowth.Evaluate(depthRatio) * (beginEventDelay - endEventDelayDelay) + endEventDelayDelay;
        Random.InitState((int)Time.time);
        eventDelay = avTime * Random.Range(1.0f - randomnessRatio, 1.0f + randomnessRatio);
    }

    private EventType PickEventType()
    {
        Lottery<EventType> eventTypeLottery = new Lottery<EventType>(eventTypes.Count);
        for(int i = 0; i < eventTypes.Count; i++)
        {
            float rate = eventTypes[i].distribution.Evaluate(depthRatio) * (eventTypes[i].maxRate - eventTypes[i].minRate) + eventTypes[i].minRate;
            eventTypeLottery.Add(eventTypes[i], rate);
        }
        return eventTypeLottery.Pick();
    }

    public sEvent GenerateEvent()
    {
        EventType eT = PickEventType();
        sEvent newEvent = null;

        if (eT.type is RepairHatchEvent)
        {
            RepairStation rS = eT.stations.GetRandomStation() as RepairStation;
            if (rS == null)
                return null;                //means all stations are broken

            newEvent = ScriptableObject.CreateInstance<RepairHatchEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
            activeEvents.Add(newEvent);
            (newEvent as RepairHatchEvent).station = rS;
            newEvent.InitiateEvent(this);
            if (rS.location != map.playerLocation)
            {
                diagH.DisplayRepairMessage(newEvent as RepairHatchEvent);
            }

        }
        else if (eT.type is RepairPipeEvent)
        {
            RepairStation rS = eT.stations.GetRandomStation() as RepairStation;
            if (rS == null)
                return null;                //means all stations are broken

            newEvent = ScriptableObject.CreateInstance<RepairPipeEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
            activeEvents.Add(newEvent);
            (newEvent as RepairPipeEvent).station = rS;
            newEvent.InitiateEvent(this);
            if (rS.location != map.playerLocation)
            {
                diagH.DisplayRepairMessage(newEvent as RepairPipeEvent);
            }

        }
        else if (eT.type is RepairPumpEvent)
        {
            RepairStation rS = eT.stations.GetRandomStation() as RepairStation;
            if (rS == null)
                return null;                //means all stations are broken

            newEvent = ScriptableObject.CreateInstance<RepairPumpEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
            activeEvents.Add(newEvent);
            (newEvent as RepairPumpEvent).station = rS;
            newEvent.InitiateEvent(this);

            if (rS.location != map.playerLocation)
            {
                diagH.DisplayRepairMessage(newEvent as RepairPumpEvent);
            }

        }

        return newEvent;
    }

    public void GenerateScriptedEvent()
    {
        if(currentScriptedEventIndex >= ScriptedStations.Length)
        {
            NotificationManager.Instance.QueueNotificationMessage("End of Practice Mode\nGood Luck!");
            this.DelayedCall(() => {
                ScriptedDone = true;
                map.ResetSpeed();
            }, 5f);
            return;
        }
        else
        {
            RepairStation rS = ScriptedStations[currentScriptedEventIndex] as RepairStation;
            if (rS == null)
                return;

            sEvent newEvent = null;
            if (rS.GetRepairEventType() == RepairEvent.RepairEventType.Hatch)
            {
                newEvent = ScriptableObject.CreateInstance<RepairHatchEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
                activeEvents.Add(newEvent);
                (newEvent as RepairHatchEvent).station = rS;
                newEvent.InitiateEvent(this);

                if (rS.location != map.playerLocation)
                {
                    diagH.DisplayRepairMessage(newEvent as RepairHatchEvent);
                }
            }

            else if (rS.GetRepairEventType() == RepairEvent.RepairEventType.Pipe)
            {
                newEvent = ScriptableObject.CreateInstance<RepairPipeEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
                activeEvents.Add(newEvent);
                (newEvent as RepairPipeEvent).station = rS;
                newEvent.InitiateEvent(this);

                if (rS.location != map.playerLocation)
                {
                    diagH.DisplayRepairMessage(newEvent as RepairPipeEvent);
                }
            }

            else if (rS.GetRepairEventType() == RepairEvent.RepairEventType.Pump)
            {
                newEvent = ScriptableObject.CreateInstance<RepairPumpEvent>(); //new RepairEvent(); // Instantiate<sEvent>(eT.type);
                activeEvents.Add(newEvent);
                (newEvent as RepairPumpEvent).station = rS;
                newEvent.InitiateEvent(this);

                if (rS.location != map.playerLocation)
                {
                    diagH.DisplayRepairMessage(newEvent as RepairPumpEvent);
                }
            }


            currentScriptedEventIndex++;
        }        
    }

    public void UnsubscribeEvent(sEvent ev)
    {
        activeEvents.Remove(ev);
        Destroy(ev);
        if (!ScriptedDone)
        {
            this.DelayedCall(() => { GenerateScriptedEvent(); }, delayBetweenScripted);
        }
    }

    public void UpdateStationSprites()
    {
        foreach(EventType type in eventTypes)
        {
            foreach (RepairStation station in type.stations.stations)
            {
                station.UpdateSprite();
            }
        }
    }

    public bool GetInTutorial() { return !ScriptedDone; }
}
