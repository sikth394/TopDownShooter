using System.Collections;
using System.Collections.Generic;
using TDGP.Demo;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private WaveSpawner waveSpawner;
    private GameObject weaponSpawns;

    private void Start()
    {
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        weaponSpawns = GameObject.Find("WeaponSpawns");

    }

   

    public void  WeaponSpawner(int waveNum) //case number in switch is how much waves have passed 
    {
        Debug.Log("weapon spawner was called with" + waveNum);
        switch (waveNum)
        {
            case 0:
                weaponSpawns.transform.GetChild(0).gameObject.SetActive(true); //pistol
                break;

            case 2:
                weaponSpawns.transform.GetChild(2).gameObject.SetActive(true); //shotgun
                break;

            case 4:
                weaponSpawns.transform.GetChild(1).gameObject.SetActive(true); //gatling gun
                break;
        }
    }
}
