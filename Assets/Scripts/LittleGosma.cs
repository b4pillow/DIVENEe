using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Unity.Mathematics;

public class LittleGosma : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float recoilLength;
    [SerializeField] private float recoilFactor;

    private float recoilTimer;
    private bool isRecoiling = false;
    private Rigidbody2D rb;

    public int Damage = 10;

    
    public GameObject orbe;
    private float dropChance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0)
        {
            float dropValue = UnityEngine.Random.Range(0, 1);
            if (dropValue <= dropChance)
            {
                Instantiate(orbe, transform.position, quaternion.identity);
            }
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

    public void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce)
    {
        health -= damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(-hitForce * recoilFactor * hitDirection);
        }
    }
}