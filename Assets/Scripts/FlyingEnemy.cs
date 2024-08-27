using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float PatrolSpeed = 5f;
    public float ChaseSpeed = 10f;
    public float HorizontalPatrolDistance = 10f;
    public float attackRange = 15f;
    public float diveAttackdHeight = 1.5f;
    private Vector2 initialPosition;//linha em observação
    private int PatrolDirection = 1;
    private bool isChasing = false;
    private Transform Player; //linha em observação

    void Start()
    {
        initialPosition = transform.position;
        //Player = gameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing)
        {
           //chase.Player();
        }
        else
        {
            //Patrol();
        }
        //detectPlayer();
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * PatrolSpeed * PatrolDirection * Time.deltaTime);
        float HorizontalDistanceTravelled = math.abs(transform.position.x - initialPosition.x);
        if(HorizontalDistanceTravelled >= HorizontalPatrolDistance)
        {
           PatrolDirection *= -1;
        }
    }

    void detectPlayer()
    {

    }

    void ChasePlayer()
    {

    }
}