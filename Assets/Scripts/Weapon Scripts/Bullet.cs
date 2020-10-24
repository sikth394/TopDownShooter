
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
            GameObject effect = Instantiate(EnemyHitEffect, transform.position, Quaternion.identity);

            switch (collision.gameObject.name)
            {

                
                case "Zombie(Clone)":
                    collision.gameObject.GetComponent<zombie>().TakeDamage(1);
                    Debug.Log("hit zombie");

                    break;

                case "Skeleton(Clone)":
                    collision.gameObject.GetComponent<Skeleton>().TakeDamage(1);
                    effect.transform.localScale = new Vector3(0.5f, 0.5f);

                    Debug.Log("hit skele");

                    break;

            }
            GameObject.Destroy(gameObject);
            Destroy(effect, 0.1f);
        }

        Destroy(gameObject,10f);

    }
}
