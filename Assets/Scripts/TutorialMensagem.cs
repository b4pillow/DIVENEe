using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMensagem : MonoBehaviour
{
    public float radious; // Raio da área de interação
    public GameObject interact; // Objeto de interface que será ativado
    public LayerMask playerLayer; // Camada do jogador para detectar colisão
    private Animator anim;

    bool onRadious;
    bool interactActivated; // Variável para verificar se o objeto já foi ativado

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Interact(); // Chamando a função Interact na física
    }

    public void Interact()
    {
        // Verifica se o jogador está dentro do raio especificado
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
        if(hit != null && !interactActivated)
        {
            interact.SetActive(true);// Ativa o GameObject de interação
            onRadious = true;
            interactActivated = true; // Marca como ativado
            anim.SetTrigger("FadeIn");
        }
        else if(hit == null)
        {
            onRadious = false;
            // Não desativa o objeto `interact` quando o jogador sai da área
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o raio de detecção no editor para visualização
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}