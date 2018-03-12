using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    [SerializeField] float playerMaxHealth;

    private float currentHealth;

    private void Start()
    {
        currentHealth = playerMaxHealth;
    }

    public void addHealth(float amountToAdd)
    {
        if(currentHealth < playerMaxHealth)
        {
            currentHealth += amountToAdd;
            if(currentHealth > playerMaxHealth) currentHealth = playerMaxHealth;
        }
    }

    public void removeHealth(float damage)
    {
        //No need to check if negative because negative or zero means death
        currentHealth -= damage;
    }

    public float getHealth()
    { return Mathf.Floor(currentHealth / playerMaxHealth *100);    }
}
