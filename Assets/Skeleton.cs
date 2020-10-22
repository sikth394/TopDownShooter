using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{


    //public Transform Player;
    private Rigidbody2D rb;

    private float moveSpeed; //fields regarding move speed of the zombie
    public float MoveSpeedMin;
    public float MoveSpeedMax;

    Transform target;   //player

    public static float attackRadious = 0.66f;
    Animator animator;
    public int maxHealth = 4;
    public int currentHealth;
    public float handRange = 0.2f;
    public LayerMask playerLayer;
    private bool canAttack;
    //params only relevent when player is alive - 
    Vector3 direction;
    float angle;
    float distance = attackRadious+3;
    public uint attackID;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = PlayerManager.instance.player.transform;
        currentHealth = maxHealth;
        moveSpeed = Random.Range(MoveSpeedMin, MoveSpeedMax);


    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            direction = target.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f;
            distance = Vector3.Distance(target.position, transform.position);
        }
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) != true)
        {
            if (rb != null)
            {
                rb.rotation = angle;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }



        if (distance <= attackRadious && target != null && gameObject.GetComponent<BoxCollider2D>())
        {
            Attack();

        }
        else
        {
            //Debug.Log("distance" + distance);
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

    private void FixedUpdate()
    {
        
    }


    void Attack()
    {

        //Debug.Log("Im in attack state");
        animator.SetBool("attack", true);
        attackID = (uint)Random.Range(0, uint.MaxValue);
        if (target.GetComponent<PlayerHealth>().hitID != attackID)
        {

            target.GetComponent<PlayerHealth>().TakeDamage(1);
            target.GetComponent<PlayerHealth>().hitID = attackID;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        if (rb != null)
        {

            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            //Debug.Log(Time.deltaTime);

            //Debug.Log("Im in move character");
            //Debug.Log("movespeed is " + moveSpeed);
            //Debug.Log("distance " + distance);
            //Debug.Log("direction " + direction);

        }
    }

 

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            animator.SetTrigger("death");
            Destroy(gameObject, 0.5f);
        }
    }



}
