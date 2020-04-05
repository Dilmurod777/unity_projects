using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float speed;
    public float timeBetweenAttacks;

    [HideInInspector]
    public Transform player;

    public int pickupChange;
    public float pickupLifetime;
    public GameObject[] pickups;

    public GameObject deathEffect;
    public GameObject bloodEffect;
    public float bloodLifetime;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber <= pickupChange)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                GameObject createdPickup = (GameObject)Instantiate(randomPickup, transform.position, transform.rotation);
                Destroy(createdPickup, pickupLifetime);
            }

            GameObject createdBlood = (GameObject)Instantiate(bloodEffect, transform.position, transform.rotation);
            Destroy(createdBlood, bloodLifetime);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
