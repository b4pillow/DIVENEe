using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PlayerBullet : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    private Vector3 moveDirection;
    private float lifeTime;
    
    public void SetBullet(Vector3 direction)
    {
        moveDirection = direction.normalized;
        lifeTime = 10;
    }
    
    void Update()
    {
        Move();
        if ((lifeTime -= Time.deltaTime) <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().EnemyHit(damage, (transform.position - other.transform.position).normalized, 100);
        }

        if (other.TryGetComponent(out Gate gateComponent))
        {
            gateComponent.EnemyHit(damage);
        }
        Destroy(gameObject);
    }
}
