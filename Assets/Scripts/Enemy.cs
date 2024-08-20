using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float recoilLength;
    [SerializeField] private float recoilFactor;

    private float recoilTimer;
    private bool isRecoiling = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Correção do nome do componente
    }

    void Start()
    {
        // Iniciar lógica do inimigo, se necessário
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (isRecoiling)
        {
            recoilTimer += Time.deltaTime;
            if (recoilTimer > recoilLength)
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
    }

    public void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce) // Correção no tipo Vector2
    {
        health -= damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(-hitForce * recoilFactor * hitDirection);
        }
    }
}