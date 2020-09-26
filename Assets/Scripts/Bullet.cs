using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject EnemyHitEffect;
    public GameObject EnvHitEffect;


    //TODO: test if this works

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
            collision.gameObject.GetComponent<zombie>().TakeDamage(1);
            GameObject effect = Instantiate(EnemyHitEffect, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
            Destroy(effect, 0.1f);
        }
        Destroy(gameObject);

    }
}
