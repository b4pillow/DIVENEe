using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float animationSpeed = 10f;
    private bool canHit;

    public int Damage = 10;

    private void Awake()
    {
        canHit = true;
    }

    void Update()
    {
        if (canHit)
        {
            if (health <= 0)
            {
                StartCoroutine(OpenGateAndDestroy(1)); // Chama a corrotina
            }    
        }
    }

    public void EnemyHit(float damageDone)
    {
        if (!canHit) return;
        health -= damageDone;
    }

    private IEnumerator OpenGateAndDestroy(float animationDuration)
    {
        canHit = false;
        while(animationDuration > 0)
        {
            animationDuration -= Time.deltaTime;
            transform.position += (animationSpeed * Time.deltaTime * Vector3.up);
            yield return null;
        }
        Destroy(gameObject); // Destroi o portão após a animação
    }
    
    
}