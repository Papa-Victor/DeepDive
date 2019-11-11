using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class sEvent: ScriptableObject
{
    virtual public void Tick(float deltaTime) { }

    abstract public void InitiateEvent(EventManager evMng);
    abstract public void Interacting(bool result = true);
    abstract public void EventResolved(bool result = true);  
}
