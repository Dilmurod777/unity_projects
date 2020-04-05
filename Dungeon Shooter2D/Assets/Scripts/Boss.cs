using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float health;
    public Enemy[] enemies;
    public float spawnOffset;

    public GameObject deathEffect;
    public GameObject bloodEffect;

    private float halfHealth;
    private Animator anim;

    public int damage;

    private Slider healthBar;

    private SceneTransitions sceneTransitions;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;

        sceneTransitions = GameObject.FindObjectOfType<SceneTransitions>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        if(health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            GameObject createdBlood = (GameObject)Instantiate(bloodEffect, transform.position, transform.rotation);
            Destroy(createdBlood, 2.0f);
            Destroy(gameObject);

            healthBar.gameObject.SetActive(false);

            sceneTransitions.LoadScene("Win");
        }

        if(health <= halfHealth)
        {
            anim.SetTrigger("run");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
