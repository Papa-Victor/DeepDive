using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public float hullIntegrety;


    public Slider healthSlider;
    public Image CriticalHealth;
    public float flashSpeed = 1f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    private Map playerData = null;


    private void Awake()
    {
        currentHealth = startingHealth;
        hullIntegrety = startingHealth;
    }


    public void SetMap(Map m)
    {
        playerData = m;
        //playerData.OnStateUpdate.AddListener(UpdateHealth);
        //UpdateHealth();
    } 

    private void Update()
    {
        currentHealth = playerData.GetHealth();
        healthSlider.value = currentHealth;
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage();
        }*/
    }

    /*
    public void UpdateHealth()
    {
        currentHealth = playerData.GetHealth();
        Debug.Log(currentHealth);
        hullIntegrety = playerData.GetHullIntegrety();
        healthSlider.value = currentHealth;
    }*/

    public void TakeDamage()
    {
        /*
        float damage = 10;
        currentHealth -= damage;
        healthSlider.value = currentHealth;*/
    }
}
