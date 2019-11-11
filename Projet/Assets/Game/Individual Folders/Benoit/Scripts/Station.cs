using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Station : MonoBehaviour
{
    public Room location;

    protected bool interactible = false;
    protected bool interactedWith = false;

    private bool hasEvent = false;
    private sEvent attachedEvent = null;
    protected PlayerStationHandler sH = null;
   
    public void AttachEvent(sEvent s)
    {
        attachedEvent = s;
        hasEvent = true;
    }
    public void DetachEvent()
    {
        attachedEvent = null;
        hasEvent = false;
    }
    public sEvent GetEvent()
    {
        return attachedEvent;
    }
    public bool HasActiveEvent()
    {
        return attachedEvent != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonoBehaviour[] list = collision.gameObject.GetComponents<MonoBehaviour>();
        collision.gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is PlayerController)
            {
                if (sH == null)
                    sH = mb.GetComponent<PlayerStationHandler>();

                sH.SethighlighStation(this);
                interactible = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!interactible) return;

        MonoBehaviour[] list = collision.gameObject.GetComponents<MonoBehaviour>();
        collision.gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is PlayerController)
            {
                if (sH == null)
                    sH = mb.GetComponent<PlayerStationHandler>();

                sH.ResetHighlightStation();
                interactible = false;
            }
        }
    }
    

    abstract public void Interact(PlayerController pc);
}