using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public static event Action<int> OnMoedaColetada;

    private static int totalMoedasColetadas = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jogador"))
        {
            totalMoedasColetadas++;

            OnMoedaColetada?.Invoke(totalMoedasColetadas);
            
            gameObject.SetActive(false);
        }
    }
}