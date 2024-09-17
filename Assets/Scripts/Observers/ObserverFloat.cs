using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverFloat : MonoBehaviour
{
    public event Action<float> OnFloatMudou;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float novoValor = UnityEngine.Random.Range(0f, 100f);
            NotificarMudanca(novoValor);
        }
    }

    private void NotificarMudanca(float valor)
    {
        OnFloatMudou?.Invoke(valor);
    }
}
