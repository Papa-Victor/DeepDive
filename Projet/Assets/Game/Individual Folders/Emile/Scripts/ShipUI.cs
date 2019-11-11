using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    public AudioSource audioBeep;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.75f);
    public Image CriticalHealth;
    public Slider healthSlider;
    private bool criticalStarted;

    void Start()
    {
        criticalStarted = false;
    }

    void Update()
    {
        if (healthSlider.value < 35 && !criticalStarted)
        {
            StartCoroutine(Critical());
            audioBeep.Play();
        }
    }

    IEnumerator Critical()
    {
        criticalStarted = true;
        CriticalHealth.color = flashColor;
        yield return new WaitForSeconds(0.5f);
        CriticalHealth.color = Color.clear;
        yield return new WaitForSeconds(0.5f);
        criticalStarted = false;
    }
}
