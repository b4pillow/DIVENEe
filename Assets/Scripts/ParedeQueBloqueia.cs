using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedeQueBloqueia : MonoBehaviour
{
    public Transform[] posicoes;
    public float speed;

    void Update()
    {
        if (Botão.pressionado == true || transform.position.y <= posicoes[1].position.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed); // Move para cima
        }
        if (Botão.pressionado == false || transform.position.y >= posicoes[0].position.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed); // Move para baixo
        }
    }
}