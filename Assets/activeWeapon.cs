using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeWeapon : MonoBehaviour
{

    public int SelectedWeapon;
    public Sprite[] sprites;
    public SpriteRenderer spriteR; // player sprite renderer
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("SoldierSprites");
        spriteR = transform.GetComponentInParent<SpriteRenderer>();
        Debug.Log(spriteR.gameObject.name);
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                transform.GetComponentInParent<Shooting>().firePoint = weapon.GetChild(0);
                transform.GetComponentInParent<Shooting>().GunFlashPrefab = weapon.GetChild(1).GetChild(0).gameObject;
                if (SelectedWeapon == 0)
                {
                    spriteR.sprite = sprites[2];
                    Debug.Log("i changed the sprite");
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        
    }
}
