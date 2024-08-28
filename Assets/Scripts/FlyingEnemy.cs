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
    private Transform player; //linha em observação

    void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing)
        {
           chasePlayer();
        }
        else
        {
            Patrol();
        }
        detectPlayer();
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
        float DistancetoPlayer = Vector2.Distance(transform.position,player.position);

        if(DistancetoPlayer  <= attackRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

    }

    void chasePlayer()
    {
        Vector2 DirectiontoPlayer = (player.position - transform.position).normalized;
        float DistancetoPlayer = Vector2.Distance(transform.position,player.position);

        if(DistancetoPlayer < diveAttackdHeight)
        {
           transform.position = Vector2.MoveTowards(transform.position, player.position, ChaseSpeed * Time.deltaTime);
        }
        else
        {
           Vector2 GroundAttackPosition = new Vector2(player.position.x,transform.position.y);
           transform.position = Vector2.MoveTowards(transform.position,GroundAttackPosition, ChaseSpeed * Time.deltaTime);
        }
    }
}