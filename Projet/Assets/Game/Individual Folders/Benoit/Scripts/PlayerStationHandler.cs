using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.DesignPattern;

public class PlayerStationHandler : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private Station highlightStation = null;
    private Station actStation;
    private Station LastHighlightedStation;

    private PlayerController pc = null;

    public void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    public void Interact()
    {
        if (highlightStation != null)
        {
            highlightStation.Interact(pc);
        }
    }

    public void SethighlighStation(Station station)
    {
        if (highlightStation == station)
            return;

        if(highlightStation != null)
        {
            if ((station.transform.position - transform.position).sqrMagnitude <= (highlightStation.transform.position - transform.position).sqrMagnitude)
            {
                SetHighlightStation(station);
                return;
            }
            else return;
        }
        else
        {
            SetHighlightStation(station);
            return;
        }
    }

    public void ResetHighlightStation()
    {
        TooltipPrompter.Instance.SetPressX(false);
        highlightStation = null;                 
    }

    public void SetHighlightStation(Station station)
    {
        ResetHighlightStation();
        if (station is Door || station.HasActiveEvent())
        {
            TooltipPrompter.Instance.SetPressX(true);
        }     
        highlightStation = station;
    }

    public Station GetHighlightStation() { return highlightStation; }
}
