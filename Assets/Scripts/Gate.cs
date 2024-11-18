using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float recoilLength;
    [SerializeField] private float recoilFactor;

    private float recoilTimer;
    private bool isRecoiling = false;
    private Rigidbody2D rb;
    private Animator anim;

    public int Damage = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Correção do nome do componente
        anim = GetComponent<Animator>();  // Garantir que o Animator seja atribuído
    }

    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(OpenGateAndDestroy()); // Chama a corrotina
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

    private IEnumerator OpenGateAndDestroy()
    {
        anim.SetTrigger("OpenGate"); // Aciona a animação de abertura do portão
        yield return new WaitForSeconds(2f); // Espera 2 segundos para garantir a execução da animação
        Destroy(gameObject); // Destroi o portão após a animação
    }
}