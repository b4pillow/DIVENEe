using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingEnemy : MonoBehaviour
{
    public float Speed = 1f;
    public float patrolSpeed = 2f;           
    public float chaseSpeed = 7f;            
    public float patrolDistance = 5f;       
    public float detectionRange = 10f;       
    public float jumpForce = 5f;            
    public Vector2 attackDirection = new Vector2(1f, 1f);  

    private Vector3 initialPosition;        
    private int patrolDirection = 1;       
    public bool isChasing = false;         
    private Rigidbody2D rb;                
    private Transform player;              

    void Start()
    {
        initialPosition = transform.position;  
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        rb = GetComponent<Rigidbody2D>();     
    }

    void Update()
    {
        if (isChasing == false) //Obs.
        {
            Patrol();
        }

        DetectPlayer();
    }

    void Patrol()
    {
        transform.Translate(Vector3.right * patrolSpeed * patrolDirection * Time.deltaTime);

        float distanceFromStart = Mathf.Abs(transform.position.x - initialPosition.x);

        if (distanceFromStart >= patrolDistance)
        {
            patrolDirection *= -1;
            transform.localScale = new Vector3( -1 * transform.localScale.x, transform.localScale.y, transform.localScale.z); // Inverte o sprite do inimigo
        }
    }

    void DetectPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; 
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isChasing)
        {
            isChasing = true; //Obs.
            StartCoroutine(JumpAttack());
        }
    }

    IEnumerator JumpAttack()
    {
       // Vector2 directionToPlayer = (player.position - transform.position).normalized;

        //Vector2 jumpDirection = new Vector2(directionToPlayer.x * attackDirection.x, attackDirection.y).normalized;

        //rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(2);
       // rb.AddForce(new Vector2(jumpDirection.x * chaseSpeed, jumpDirection.y * jumpForce), ForceMode2D.Impulse);

        if(player.position.x < transform.position.x)
        {
            //pulo pra esquerda
            rb.AddForce(new Vector2(-1,1) * jumpForce,ForceMode2D.Impulse); 
        }
        else
        {
            //pulo pra direita
             rb.AddForce(new Vector2(1,1) * jumpForce,ForceMode2D.Impulse);

        }

        yield return new WaitForSeconds(2);
        isChasing = false;
        StopCoroutine(JumpAttack()); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isChasing = false;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inimigo atingiu o jogador!");
            isChasing = false;
        }
    }
}