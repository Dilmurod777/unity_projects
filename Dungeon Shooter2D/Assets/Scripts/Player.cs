using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public Transform weaponSpawner;

    public float health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveAmount;

    public Animator hurtPanelAnim;
    public AudioClip hurtSound;

    private AudioSource source;

    private SceneTransitions sceneTransitions;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        sceneTransitions = GameObject.FindObjectOfType<SceneTransitions>();
    }


    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = speed * moveInput.normalized;

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        source.clip = hurtSound;
        source.Play();
        
        UpdateHealthUI(Mathf.FloorToInt(health));
        hurtPanelAnim.SetTrigger("hurt");

        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, weaponSpawner.position, Quaternion.Euler(0, 0, -51.937f), transform);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if(health + healAmount > 5)
        {
            health = 5.0f;
        }
        else
        {
            health += healAmount;
        }

        UpdateHealthUI(Mathf.FloorToInt(health));
    }
}
