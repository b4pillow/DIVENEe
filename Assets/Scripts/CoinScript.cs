using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int CoinPoint = 0; 
    private void OnTriggerEnter2D(Collider2D CP) 
    {
        if (CP.gameObject.CompareTag("Player"))
        {
            Debug.Log("moeda Coletada");
            CoinPoint ++;
            Destroy(CP.gameObject);
        }

    }
}