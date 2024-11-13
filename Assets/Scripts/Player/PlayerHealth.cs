using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    void Start()
    {
        if(healthBar == null){
            Debug.LogError("Health bar is not assigned");
        }
        if(maxHealth <= 0){
            Debug.LogWarning("Max health is not set, setting to 100");
            maxHealth = 100f;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float fillAm = Mathf.Clamp(health / maxHealth, 0, 1);
        healthBar.fillAmount = fillAm;

        Debug.Log("Health: " + fillAm);
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    private void Die(){
        Debug.Log("Player has died");
    }
}
