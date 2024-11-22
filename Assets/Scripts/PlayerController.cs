using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   [Header("Movimentos")]
   [SerializeField] private float walkSpeed = 1;
   [SerializeField] private float jumpForce = 45;
   private float jumpBufferCounter = 0;
   [SerializeField] private float jumpBufferFrames;
   private float coyoteTimeCounter = 0;
   [SerializeField] private float coyoteTime;


   bool knockbacking;




   [Header("Ground Check")]
   [SerializeField] private Transform groundCheck;
   [SerializeField] private float groundCheckY = 0.2f;
   [SerializeField] private float groundCheckX = 0.5f;
   [SerializeField] private LayerMask whatIsGround;


   PlayerStateList pState;


   public Rigidbody2D rb { get; private set; }
   private float directionX, directionY;
   private float gravity;
   private Animator anim;
   private bool canDash = true;
   private bool dashed = false;


   public Vector3 Position;


   private Vector3 lastCheckpointPosition;
   private bool checkpointSaved = false;








   [Header("Singleton")]
   public static PlayerController Instance;
   private void Awake()
   {
       if (Instance != null && Instance != this)
       {
           Destroy(gameObject);
       }
       else
       {
           Instance = this;
       }
   }


   [Header("Dash")]
   [SerializeField] private float dashSpeed = 20f;
   [SerializeField] private float dashTime = 0.2f;
   [SerializeField] private float dashCooldown = 1f;
  
   [Header("Attacking")]
   bool attack = false;
   private float timeBetweenAttack, timeSinceAttack;
   [SerializeField] private Transform sideAttackTransform;
   [SerializeField] private Vector2 SideAttackArea;
   [SerializeField] private LayerMask attackableLayer;
   [SerializeField] private float damage;
   [SerializeField] float attackingTime = 0.4f;
   private int specialContagem;
   public Transform firePoint;
   public PlayerBullet Special;
   public TMPro.TextMeshProUGUI texto;
  
   void Start()
   {
       pState = GetComponent<PlayerStateList>();
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       gravity = rb.gravityScale;
       Position = transform.position;
       knockbacking = false;
       specialContagem = 0;
   }


   private void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireCube(sideAttackTransform.position, SideAttackArea);
   }


   // Update is called once per frame
    void Update()
   {
       if (pState.attacking && !knockbacking)
       {
           rb.velocity = Vector2.zero;
           return;
       }


       GetInputs();
       UpdateJumpVariables();
       if (pState.dashing) return;
       Flip();
       Move();
       Jump();
       StartDash();
       if (attack)
       {
           StartCoroutine(Attack());
       }
   }


   void GetInputs()
   {
       directionX = Input.GetAxisRaw("Horizontal");
       directionY = Input.GetAxisRaw("Vertical");
       attack = Input.GetMouseButtonDown(0);
   }


   void Flip()
   {
       if (DialogueControl.Instance.isTyping == false && Time.timeScale != 0f)
       {
           Vector3 localScale = transform.localScale;


           if (directionX < 0)
           {
               localScale.x = -Mathf.Abs(localScale.x);
           }
           else if (directionX > 0)
           {
               localScale.x = Mathf.Abs(localScale.x);
           }


           transform.localScale = localScale;  
       }
   }


   private void Move()
   {
       if (DialogueControl.Instance.isTyping == false && Time.timeScale != 0f && !knockbacking)
       {
           if(pState.attacking == false)
           {
               //rb.AddForce(new Vector2(walkSpeed * directionX, rb.velocity.y), ForceMode2D.Force);
               rb.velocity = new Vector2(walkSpeed * directionX, rb.velocity.y);
               anim.SetBool("Walking", rb.velocity.x != 0 && Grounded());
           }
       }
   }


   void StartDash()
   {
       if (DialogueControl.Instance.isTyping == false && !knockbacking)
       {
           if (Input.GetButtonDown("Dash") && canDash && !dashed)
           {
               StartCoroutine(Dash());
               dashed = true;
           }


           if (Grounded())
           {
               dashed = false;
           }
       }
   }


   IEnumerator Dash()
   {
       canDash = false;
       pState.dashing = true;
       anim.SetTrigger("Dashing");
       rb.gravityScale = 0;
       rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
       yield return new WaitForSeconds(dashTime);
       rb.gravityScale = gravity;
       pState.dashing = false;
       yield return new WaitForSeconds(dashCooldown);
       canDash = true;
   }


   IEnumerator Attack()
   {
      
       if (DialogueControl.Instance.isTyping == false && !knockbacking)
       {
           if (pState.attacking) yield break;
           pState.attacking = true;
  
           timeSinceAttack += Time.deltaTime;
           if (attack && timeSinceAttack >= timeBetweenAttack)
           {
               if (directionY == 0 || (directionY < 0 && Grounded()))
               {
                   Hit(sideAttackTransform, SideAttackArea);
               }
          
               timeSinceAttack = 0;
               pState.attacking = true;
               anim.SetTrigger("Attacking");
               yield return new WaitForSeconds(attackingTime);
               pState.dashing = false;
           }


           pState.attacking = false;
       }
   }




   private void Hit(Transform attackTransform, Vector2 attackArea)
   {
       Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(attackTransform.position, attackArea, 0, attackableLayer);


       for (int i = 0; i < objectsToHit.Length; i++)
       {
           if (objectsToHit[i].GetComponent<Enemy>() != null)
           {
               objectsToHit[i].GetComponent<Enemy>().EnemyHit(damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
               SpecialCount();
           }

           if (objectsToHit[i].TryGetComponent(out Gate gateComponent))
           {
               gateComponent.EnemyHit(damage);
           }
       }
   }


   private void SpecialCount()
   {
       specialContagem++;
       if (specialContagem >= 5)
       {
           ExecutarSpecial();
           specialContagem = 0;
       }


       UpdateNumber();
   }


   void UpdateNumber()
   {
       if (specialContagem == 0)
       {
           StartCoroutine(CountingEffect());
       }
       else
       {
           texto.text = $"{specialContagem}";
       }


   }


   IEnumerator CountingEffect()
   {
       texto.text = "Soltando Poder";
       yield return new WaitForSeconds(0.5f);
       texto.text = $"{specialContagem}";
   }
  
   public IEnumerator KnockbackEffect(Vector2 direction)
   {
       rb.velocity = direction * 20;
       knockbacking = true;
       yield return new WaitForSeconds(0.2f);
       knockbacking = false;
       rb.velocity = Vector2.zero;
   }

    private void ExecutarSpecial()
   {
       PlayerBullet playerBullet = Instantiate(Special, firePoint.position, firePoint.rotation);
       float direction = Mathf.Clamp(transform.localScale.x, -1, 1);
       playerBullet.SetBullet(new(direction, 0, 0));
   }

   public bool Grounded()
   {
       if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround)
           || Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
           || Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
       {
           return true;
       }
       else
       {
           return false;
       }
   }
   void Jump()
   {
       if (DialogueControl.Instance.isTyping == false)
       {
           if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
           {
               rb.velocity = new Vector2(rb.velocity.x, 0);
               pState.jumping = false;
           }


           if (!pState.jumping && Grounded())
           {
               if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
               {
                   rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                   pState.jumping = true;
                   jumpBufferCounter = 0;
               }
           }


           anim.SetBool("Jumping", !Grounded());
       }
   }




   void UpdateJumpVariables()
   {
       if (Grounded())
       {
           pState.jumping = false;
           coyoteTimeCounter = coyoteTime;
           jumpBufferCounter = 0;
       }
       else
       {
           coyoteTimeCounter -= Time.deltaTime;
       }


       if (Input.GetButtonDown("Jump"))
       {
           jumpBufferCounter = jumpBufferFrames;
       }
       else
       {
           jumpBufferCounter = jumpBufferCounter - Time.deltaTime * 10;
       }
   }


     public void SaveCheckpoint(Vector3 checkpointPosition)
   {
       lastCheckpointPosition = checkpointPosition;
       checkpointSaved = true;
   }

}

