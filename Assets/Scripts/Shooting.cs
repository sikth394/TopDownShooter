using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject GunFlashPrefab;

    public float bulletforce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){

            Shoot();
        }
        
    }

    void Shoot()
    {
        GameObject GunFlash = Instantiate(GunFlashPrefab, firePoint.position, firePoint.rotation);
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse) ;
        Destroy(GunFlash, 0.5f);
        Destroy(Bullet, 10f);

    }
}

