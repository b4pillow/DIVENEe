using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystemPlayer : MonoBehaviour
{
    public int health;
    public int maxHealth = 20;
    [SerializeField] private LifeUI barraDeVida;
    private Animator anim;
    public GameController GC;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        barraDeVida.ChangeBar(health, maxHealth);
        anim = GetComponent<Animator>(); // Inicializa o Animator
       
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Damage(2);
        }
    }
}