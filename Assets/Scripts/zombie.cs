using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{


    //public Transform Player;
    private Rigidbody2D rb;
    public float moveSpeed;
    Transform target;
    public float attackRadious = 0.66f;
    Animator animator;
    public int maxHealth = 4;
    public int currentHealth;
    public Transform attackPoint;
    public float handRange = 0.2f;
    public LayerMask playerLayer;
    private bool canAttack;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = PlayerManager.instance.player.transform;
        currentHealth = maxHealth;

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Death")) != true)
        {
            rb.rotation = angle;
        }
    
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= attackRadious)
        {
            Attack();
           
        }
        else
        {
            animator.SetBool("attack", false); 
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetBool("move", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            direction.Normalize();
            moveCharacter(direction);
        }

    }


    void Attack()
    {
        animator.SetBool("attack", true);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, handRange, playerLayer);
        if (hitPlayer!= null)
        {
            hitPlayer[0].gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, handRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            animator.SetBool("death", true);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject,1f);
        }
    }



}
