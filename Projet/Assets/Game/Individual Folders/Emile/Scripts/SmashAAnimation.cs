using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SmashAAnimation : MonoBehaviour
{
    public Transform AButton;

    // Start is called before the first frame update
    void Start()
    {
        ArrowDownAnim();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ArrowDownAnim()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOMoveY(transform.position.y - 15f, 0.1f, false)).SetEase(Ease.Linear);
        sq.Append(transform.DOMoveY(transform.position.y, 0.1f, false)).SetEase(Ease.Linear);
        sq.SetLoops(-1);
        sq.Play();
    }
}
