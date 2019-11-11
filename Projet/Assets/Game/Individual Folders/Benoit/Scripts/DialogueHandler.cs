using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public void DisplayRepairMessage(RepairEvent ev)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Warning\n");
        switch (ev.GetRepairType())
        {
            case RepairEvent.RepairEventType.Hatch:
            sb.Append("HATCH");
                break;
            case RepairEvent.RepairEventType.Pipe:
            sb.Append("PIPE");
                break;
            case RepairEvent.RepairEventType.Pump:
            sb.Append("PUMP");
                break;
        }
        sb.Append(" broke in the ");
        switch (ev.station.location)
        {
            case Room.Front:
                sb.Append("A");
                break;
            case Room.Middle:
                sb.Append("B");
                break;
            case Room.Back:
                sb.Append("C");
                break;
        }
            
        sb.Append(" room");

        if (NotificationManager.Instance)
            NotificationManager.Instance.QueueNotificationMessage(sb.ToString());

        Debug.Log(sb.ToString());
    }


}
