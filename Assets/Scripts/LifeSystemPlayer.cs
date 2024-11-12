using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystemPlayer : MonoBehaviour
{
    public int health;
    public int maxHealth = 20;
     public float KnockbackForce = 10f;
     public float invulnerabilityDuration = 0.2f;
    private bool isInvulnerable = false; 
    [SerializeField] private LifeUI barraDeVida;
    private Animator anim;
    public GameController GC;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public PlayerController player;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        barraDeVida.ChangeBar(health, maxHealth);
        anim = GetComponent<Animator>(); // Inicializa o Animator
        spriteRenderer.GetComponent<SpriteRenderer>(); //Obs.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dano)
    {
        health -= dano;
        barraDeVida.ChangeBar(health, maxHealth);
        if (health <= 0)
        {
            GC.GameOver();
            Destroy(gameObject, 0);
        }
        
        anim.SetTrigger("TakeDamage"); // Usa o Animator diretamente
    }

    void OnCollisionEnter2D(Collision2D other )
    {
        if (other.gameObject.CompareTag("Enemy")  && !isInvulnerable)
        {
                
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            StartCoroutine(player.KnockbackEffect(knockbackDirection));
            StartCoroutine(StartInvulnerability());

            Damage(1);
        }
    }


    private System.Collections.IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;
        StartCoroutine(BlinkSprite()); 

        yield return new WaitForSeconds(invulnerabilityDuration); 

        isInvulnerable = false;
        spriteRenderer.enabled = true; 
    }

    private System.Collections.IEnumerator BlinkSprite()
    {
        while (isInvulnerable)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); 
        }
        spriteRenderer.enabled = true;
    }
}