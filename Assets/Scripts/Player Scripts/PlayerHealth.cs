using System.Collections;
using System.Collections.Generic;
using TDGP;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    Animator animator;
    private float hitLast = 0;
    private float hitDelay = 1.5f;
    public uint hitID;
    public GameOverMenu gameOver ;
   
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth; 
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        gameOver = GameObject.Find("Canvas").GetComponent<GameOverMenu>();   //once player is dead a Game Over menu pops up

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
                //GetComponent<PlayerMovement>().childAnim.SetTrigger("hit");
                animator.SetTrigger("hit");
               
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
                animator.SetFloat("body", 2);
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                Destroy(gameObject, 1f);
                gameOver.playerIsDead = true;
                



            }
           
        healthBar.SetHealth(currentHealth);
           
        hitLast = Time.time;
    }
}
