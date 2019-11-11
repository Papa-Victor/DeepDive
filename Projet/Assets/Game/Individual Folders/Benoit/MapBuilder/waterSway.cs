using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class waterSway : MonoBehaviour {
    static int count;

    public SpriteRenderer water;
    public float moveOffset = 2.0f;
    public float cycleDelay = 1.0f;

    private void Start()
    {
        float target = moveOffset;
        if (transform.position.x > 0)
            target -= moveOffset;

        water.transform.DOMoveX(water.transform.position.x + moveOffset, Random.Range(0.75f * cycleDelay, 1.25f * cycleDelay))
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo)
        .SetRelative(true);
    }
}
