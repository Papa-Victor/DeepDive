using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SmashA : MonoBehaviour
{
    public GameObject ArrowDown;

    private bool active = true;
    private bool moving = false;


    // Start is called before the first frame update
    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        ArrowDown.transform.localPosition = new Vector3(0.0f, 0.45f, 0.0f);
        active = true;
        ArrowMovement();
    }

    public void Desactivate()
    {
        active = false;
    }


    public void ArrowMovement()
    {
        if (active)
        {
            ArrowDown.transform.localPosition = new Vector3(0.0f, 0.45f, 0.0f);
            Sequence sq = DOTween.Sequence();

            sq.Append(ArrowDown.transform.DOMoveY(ArrowDown.transform.position.y - 0.25f, 0.5f, false)).SetEase(Ease.Linear);
            sq.Append(ArrowDown.transform.DOMoveY(ArrowDown.transform.position.y, 0.5f, false)).SetEase(Ease.Linear);
            sq.OnComplete(ArrowMovement);
            sq.Play();
        }
    }
}
