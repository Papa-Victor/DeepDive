using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State<T>
{
    public abstract void EnterState(T actor);

    public abstract void Execute(T actor);

    public abstract void ExitState(T actor);
}
