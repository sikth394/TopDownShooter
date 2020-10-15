using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeWeapon : MonoBehaviour
{

    public int SelectedWeapon;
    public Animator playerAnim; // player sprite renderer
    public LayerMask weaponsLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = transform.GetComponentInParent<Animator>();
        weaponsLayerMask = LayerMask.GetMask("Weapons");
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform) //cycles through weapons in 'weapon' gameObject
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                transform.GetComponentInParent<Shooting>().firePoint = weapon.GetChild(0);
                transform.GetComponentInParent<Shooting>().GunFlashPrefab = weapon.GetChild(1).GetChild(0).gameObject;
                if (weapon.gameObject.CompareTag("GatlingGun"))
                {
                    playerAnim.SetFloat("body", 0.8f);
                }
                if (weapon.gameObject.CompareTag("OneHanded"))
                {
                    playerAnim.SetFloat("body", 0.2f);
                }
                if (weapon.gameObject.CompareTag("TwoHanded"))
                {
                    playerAnim.SetFloat("body", 0.5f);
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("I've entered on collision stay");
        if (collision.gameObject.layer == 10)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SelectedWeapon = collision.GetComponent<weaponIndex>().weaponIndx;
                Debug.Log("SelectedWeapon is" + SelectedWeapon);
                SelectWeapon();
                Debug.Log("Ive changed weapons!");
            }
        }
    }
}
