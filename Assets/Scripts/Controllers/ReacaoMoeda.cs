using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReacaoMoeda : MonoBehaviour
{
     public AudioSource efeitoSonoro; 

    private void OnEnable()
    {
        Moeda.OnMoedaColetada += ReagirColetaMoeda;
    }

    private void OnDisable()
    {
        Moeda.OnMoedaColetada -= ReagirColetaMoeda;
    }

    private void ReagirColetaMoeda(int totalMoedas)
    {
        if (efeitoSonoro != null)
        {
            efeitoSonoro.Play();
        }

        Debug.Log($"O jogador pegou uma moeda! Total de moedas coletadas: {totalMoedas}");
    }

    public void OnTriggerEnter2D()
    {
     
    }

}
