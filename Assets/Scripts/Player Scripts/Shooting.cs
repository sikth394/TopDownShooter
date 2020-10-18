using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject GunFlashPrefab;
    public string activeWeapon;

    //fields regarding auto-fire (e.g gatling gun..)
    public float autoFireDelay = 0.05f;
    public float shotgunFireDelay = 1f;
    private float firelast = 0f;
    //end fields regarding auto fire


    public float bulletforce = 20f;

    // Update is called once per frame
    void Update()
    {

        switch (activeWeapon)
        {
            case "GatlingGun":
                if (Input.GetButton("Fire1") && gameObject != null && PauseMenu.GameIsPaused == false)
                {

                    if ((Time.time - firelast) > autoFireDelay)
                    {

                        Shoot();
                        firelast = Time.time;
                    }
                }
                break;

            case "OneHanded":
                {
                    if (Input.GetButtonDown("Fire1") && gameObject != null && PauseMenu.GameIsPaused == false)
                    {
                        Shoot();
                    }
                    break;
                }

            case "Shotgun":
                {

                    if (Input.GetButtonDown("Fire1") && gameObject != null && PauseMenu.GameIsPaused == false)
                    {
                        if ((Time.time - firelast) > shotgunFireDelay)
                        {
                            Shoot();
                            firelast = Time.time;
                        }
                    }
                    break;
                }
        }
    }

    void Shoot()
    {
        GameObject GunFlash = Instantiate(GunFlashPrefab, firePoint.position, firePoint.rotation);
        GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse);
        Destroy(GunFlash, 0.5f);
        Destroy(Bullet, 10f);

    }

    void Shoot (int x) //overloading of when more then one bullet needs to be fired (x amount of bullets)
    {
        GameObject GunFlash = Instantiate(GunFlashPrefab, firePoint.position, firePoint.rotation);
        Quaternion firePointRotation = firePoint.rotation;
        
        for (int i = 0; i < x; i++)
        {
            firePointRotation.z = (firePoint.rotation.z - 15f) + (15f * x);
            GameObject Bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("a new bullet was created");
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse);
            
        }
        Destroy(GunFlash, 0.5f);
    }
}

