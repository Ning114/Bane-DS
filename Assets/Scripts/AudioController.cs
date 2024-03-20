using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    public AudioSource ammo, enemyDeath, enemyShot, gunShot, health, playerHurt;


    void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playAmmoPickup()
    {
        ammo.Stop(); // Ensure previous audio isn't interrupted unnecessarily
        ammo.Play();
    }

    public void playEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }

    public void playEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }

    public void playGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }

    public void playHealthPickup()
    {
        health.Stop();
        health.Play();
    }

    public void playPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }

}
