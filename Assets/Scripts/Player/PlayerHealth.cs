using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int initialHealth;
    void Start()
    {
        initialHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage){
        initialHealth -= damage;
        if(initialHealth <= 0){
            Die();
        }
    }

    private void Die(){
        Debug.Log("Player has died");
    }
}
