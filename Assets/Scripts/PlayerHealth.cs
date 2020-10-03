using System.Collections;
using System.Collections.Generic;
using TDGP;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    Animator animator;
    private float hitLast = 0;
    private float hitDelay = 2;
    public uint hitID;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth; 
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        if ((Time.time - hitLast) < hitDelay) { 
            return;
        }
            if (currentHealth > 1)
            {
                currentHealth -= damage;
               
            }
            else 
            {
                currentHealth = 0;
                Destroy(GetComponent<PlayerMovement>());
                int childs = transform.childCount;
                for ( int i = childs -1; i>= 0; i--)
                {
                     GameObject.Destroy(transform.GetChild(i).gameObject);
                }

                animator.SetTrigger("death");
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                Destroy(gameObject, 1f);
            }
           
        healthBar.SetHealth(currentHealth);
           
        hitLast = Time.time;
    }
}
