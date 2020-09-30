using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    Animator animator;
    private float hitLast = 0;
    private float hitDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if ((Time.time - hitLast) < hitDelay)
            return;
        
            if (currentHealth >0)
            {
                currentHealth -= damage;

            }
            else
            {
                currentHealth = 0;
            }
            
            healthBar.SetHelth(currentHealth);
            //animator.SetBool("death", true);
        
        hitLast = Time.time;
    }
}
