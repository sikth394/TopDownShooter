
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject EnemyHitEffect;
    public GameObject EnvHitEffect;


    //TODO: test if this works

    private void Start()
    {
        Physics.IgnoreLayerCollision(11, 11);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Environment"))
        {
            GameObject effect = Instantiate(EnvHitEffect, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
            Destroy(effect, 0.1f);
            

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {

            //Debug.Log(collision.gameObject.name);
            switch (collision.gameObject.name)
            {
                
                case "Zombie":
                    collision.gameObject.GetComponent<zombie>().TakeDamage(1);
                    break;

                case "Skeleton":
                    collision.gameObject.GetComponent<Skeleton>().TakeDamage(1);

                    break;

            }
      
            GameObject effect = Instantiate(EnemyHitEffect, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
            Destroy(effect, 0.1f);
        }
        Destroy(gameObject,10f);

    }
}
