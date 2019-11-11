using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmarineSway : MonoBehaviour
{
    public float cycleDelay = 3.0f;

    public float swayMaxAngle = 5.0f;
    private bool isTurning = false;

    private void Update()
    {
        if(isTurning == false)
        {
            isTurning = true;

            float targetAngle;

            if (transform.rotation.z >= 0)
                targetAngle = -swayMaxAngle;
            else
                targetAngle = swayMaxAngle;


            transform.DORotate(new Vector3(0, 0, targetAngle), cycleDelay)
                .SetEase(Ease.InOutSine)
                .OnComplete(( ) => { isTurning = false; } );
        }
    }
}
