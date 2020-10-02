using System.Collections;
using System.Collections.Generic;
using TDGP;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    Animator animator;
    private float hitLast = 0;
    private float hitDelay = 1;

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
        if ((Time.time - hitLast) < hitDelay)
            return;
        
            if (currentHealth >0)
            {
                currentHealth -= damage;

            }
            else
            {
                currentHealth = 0;
                healthBar.SetHelth(currentHealth);
                Destroy(GetComponent<PlayerMovement>());
            //Destroy(GetComponent<Transform>().GetChild(0).gameObject,0.1f);
            //Destroy(GetComponent<Transform>().GetChild(2).gameObject,3f);
                foreach (Transform child in gameObject.transform)
                  {
                     GameObject.Destroy(child.gameObject);
                  }
                animator.SetTrigger("death");
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                Destroy(gameObject, 1f);
            }
            
            healthBar.SetHelth(currentHealth);
            
        
        hitLast = Time.time;
    }
}
