using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPipeStation : RepairStation
{
    public GameObject smallWater;
    public GameObject bigWater;
    public GameObject hole;

    private SpriteRenderer sp;

    private void Begin()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public override void Interact(PlayerController pc)
    {
        if (HasActiveEvent())
        {
            pc.ChangeState(PipePlayerInputState.GetInstance());
            Audio_Cues[2].Play();
            GetEvent().Interacting(true);

            TooltipPrompter.Instance.SetPressX(false);
            TooltipPrompter.Instance.SetPressRT(true);
        }
    }

    public bool Tighten()
    {
        bool temp = ((RepairPipeEvent)GetEvent()).Tighten();

        if (temp)
        {
            smallWater.SetActive(false);
            bigWater.SetActive(false);
            hole.SetActive(false);
            //Anim.SetBool("Active", false);        //Mieux de le défénir dans les changements d'états
            Audio_Cues[1].Pause();                
            Audio_Cues[2].Pause();
            Audio_Cues[3].Play();                 //Make sense si bruit de réparation, autrement same
            TooltipPrompter.Instance.SetPressRT(false);

            return true;
        }
        return false;
    }

    public void Release()
    {
        ((RepairPipeEvent)GetEvent()).Release();
    }

    override public void UpdateSprite()
    {
        switch (State)
        {
            case RepairState.Normal:
                smallWater.SetActive(false);
                bigWater.SetActive(false); 
                hole.SetActive(false);

                if (isActiveAndEnabled)
                {
                    //Audio_Cues[1].Pause();         
                    //Audio_Cues[2].Pause();
                }
                break;
            case RepairState.NewBroken:
                smallWater.SetActive(false);
                bigWater.SetActive(true);
                hole.SetActive(true);

                if (isActiveAndEnabled)
                {
                    Audio_Cues = GetComponentsInChildren<AudioSource>();
                    Audio_Cues[0].Play();
                    Audio_Cues[1].Play();
                }
                break;
            case RepairState.OldBroken:
                smallWater.SetActive(false);
                bigWater.SetActive(true);
                hole.SetActive(true);

                break;
            default:
                Debug.Log("PROBLEMUUUU"); 
                break;
        }
    }
}