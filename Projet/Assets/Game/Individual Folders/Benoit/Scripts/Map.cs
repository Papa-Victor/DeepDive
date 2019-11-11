using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CCC.DesignPattern;


public enum Room { Front, Middle, Back };

public class Map : PublicSingleton<Map>
{
    public float speed = 1;
    [SerializeField]
    private float baseSpeed;
    public float depth = 0;

    public const float maxHealth = 100;

    public float dmgTaken = 0;
    public float repairableDmg = 0;

    public Room playerLocation;

    public Camera mainCamera;

    private EventManager eventManager;

    //public UnityEvent OnStateUpdate;


    private void FixedUpdate()
    {
        float deltaY = speed * Time.deltaTime;
        Vector3 up = transform.up;
        transform.position = transform.position + (new Vector3(deltaY * up.x, deltaY * up.y, 0));
        depth += deltaY;
    }

    public void AddDamage(float fullDamage, float repairable)
    {
        if (!eventManager.GetInTutorial())
        {
            dmgTaken += fullDamage;
            repairableDmg += repairable;

            if (GetHealth() <= 0)
            {
                FindObjectOfType<Game>().GetComponent<Game>().verifyGameover();
            }
        }

        //if (OnStateUpdate != null)
         //   OnStateUpdate.Invoke();
    }

    public void RemoveDamage(float fullDamage, float repairable)
    {
        if (!eventManager.GetInTutorial())
        {
            dmgTaken -= fullDamage;
            repairableDmg -= repairable;

            if (dmgTaken < 0)
                dmgTaken = 0;
            if (repairableDmg < 0)
                repairableDmg = 0;
        }

       // if (OnStateUpdate != null)
        //    OnStateUpdate.Invoke();
    }

    public float GetHealth()
    {
        return maxHealth - dmgTaken;
    }

    public float GetHullIntegrety()
    {
        return maxHealth - repairableDmg;
    }

    public void UpdatePlayerLocation(Room room)
    {
        playerLocation = room;
    }

    public void SetEventManager(EventManager eventManager) { this.eventManager = eventManager; }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    public void StopSpeed()
    {
        speed = 0;
    }
}
