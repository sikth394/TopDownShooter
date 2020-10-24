using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    //TODO: add something like hit-delay to the bool 'attack'


    //public Transform Player;
    private Rigidbody2D rb;

    private float moveSpeed; //fields regarding move speed of the zombie
    public float MoveSpeedMin;
    public float MoveSpeedMax;

    Transform target;   //player

    public  float attackRadious;

    public float leftArmRadious;
    public float rightArmRadious;
    Animator animator;
    public int maxHealth = 4;
    public int currentHealth;

    //params only relevent when player is alive - 
    Vector3 direction;
    float angle;

    // params keeping records of player distance to places that hit the player on the skeleton
    float distance;
    float distanceToLeftArm;
    float distanceToRightArm;

    public Transform leftArm;
    public Transform rightArm;
    public Transform body;
    public uint attackID;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = PlayerManager.instance.player.transform;
        currentHealth = maxHealth;
        moveSpeed = Random.Range(MoveSpeedMin, MoveSpeedMax);
        leftArm = gameObject.transform.Find("Left Arm").transform;
        rightArm = gameObject.transform.Find("Right Arm").transform;
        //animator.applyRootMotion = false;


    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            direction = target.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f;
            distance = Vector3.Distance(target.position, body.position);
            distanceToLeftArm = Vector3.Distance(target.position, leftArm.position);
            distanceToRightArm = Vector3.Distance(target.position, rightArm.position);
        }
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) != true)
        {
            if (rb != null)
            {
                rb.rotation = angle;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                //Debug.Log("IM in rb isn't null");
            }

            else
            {

                gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            }
        }

        if (((distance <= attackRadious) || (distanceToLeftArm <= leftArmRadious) || (distanceToRightArm <= rightArmRadious)) && target != null && gameObject.GetComponent<PolygonCollider2D>())                // attack conditions
        {
            animator.SetBool("attack", true);
            Attack();

        }
        else
        {
            //Debug.Log("distance" + distance);
            animator.SetBool("attack", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            //Debug.Log("Im in Move animation");
            direction.Normalize();
            moveCharacter(direction);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            
            //Debug.Log("Im in Idle animation");
        }
    }

   


    void Attack()
    {

        //Debug.Log("Im in attack state");
        
        attackID = (uint)Random.Range(0, uint.MaxValue);
        if (target.GetComponent<PlayerHealth>().hitID != attackID)
        {

            target.GetComponent<PlayerHealth>().TakeDamage(1);
            target.GetComponent<PlayerHealth>().hitID = attackID;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));

    }



    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            animator.SetTrigger("death");
            Destroy(gameObject.GetComponent<Animator>(), 1.23f);
            Destroy(gameObject, 1.24f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(body.position, attackRadious);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(leftArm.position, leftArmRadious);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rightArm.position, rightArmRadious);
        
        
    }



}
