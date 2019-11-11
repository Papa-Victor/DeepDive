using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC;
using CCC.Utility;

public class StationsGroup : MonoBehaviour
{
    public List<Station> stations;

    public Station GetRandomStation()
    {
        List<Station> availableStations = new List<Station>();
        foreach (Station s in stations)
            if (s.HasActiveEvent() == false)
                availableStations.Add(s);

        if (availableStations.Count == 0)
            return null;
        if (availableStations.Count == 1)
            return availableStations[0];

        RandomList<Station> rd = new RandomList<Station>(stations);
        return rd.Pick();
        
    }
}
