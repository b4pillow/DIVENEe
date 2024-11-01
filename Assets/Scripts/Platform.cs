using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform plataform;
    public Transform[] posAtual;
    private int idPos;
    public float speed;
    void Start()
    {
        plataform.position = posAtual[0].position;
        idPos = 1;
    }
   
    void Update()
    {
        plataform.position = Vector3.MoveTowards(plataform.position, posAtual[idPos].position, speed * Time.deltaTime);
       
        if (plataform.position == posAtual[idPos].position)
        {
            idPos += 1;
        }

        if (idPos == posAtual.Length)
        {
            idPos = 0;
        }
    }
}
