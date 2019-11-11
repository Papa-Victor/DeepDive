using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepairStation : Station
{
    protected Animator Anim;
    [SerializeField] protected AudioSource[] Audio_Cues;
    protected RepairEvent rEvent;

    [SerializeField]
    private RepairState rState;

    [SerializeField]
    protected RepairEvent.RepairEventType type;

    public RepairState State
    {
        get { return rState; }
        set
        {
            rState = value;
            UpdateSprite();
        }
    }

    protected void Start()
    {
        rState = RepairState.Normal;
        Anim = GetComponent<Animator>();
        Audio_Cues = transform.GetComponentsInChildren<AudioSource>();
    }

    protected void Awake()
    {
        if (Anim == null) Anim = GetComponent<Animator>();
        if (Audio_Cues == null) Audio_Cues = transform.GetComponentsInChildren<AudioSource>();
    }

    virtual public void UpdateSprite()
    {
        switch (rState)
        {
            case RepairState.Normal:
                if (isActiveAndEnabled)
                {
                    Anim.SetBool("Active", false);
                }
                break;
            case RepairState.NewBroken:
                
                if (isActiveAndEnabled)
                {

                    Anim.SetBool("Active", true);

                    Audio_Cues = GetComponentsInChildren<AudioSource>();

                    Audio_Cues[0].Play();
                    Audio_Cues[1].Play();
                }
                break;
            case RepairState.OldBroken:
                if (isActiveAndEnabled)
                {
                    Audio_Cues = GetComponentsInChildren<AudioSource>();

                    Audio_Cues[0].Play();
                    Audio_Cues[1].Play();
                }
                break;
            default:
                Debug.Log("PROBLEMUUUU");
                break;
        }
    }

    public RepairEvent.RepairEventType GetRepairEventType() { return type; }

}
