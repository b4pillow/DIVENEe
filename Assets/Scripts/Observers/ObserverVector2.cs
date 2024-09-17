using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverVector2 : MonoBehaviour
{
     // Evento com um parâmetro do tipo Vector2
    public event Action<Vector2> OnVector2Mudou;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector2 novaPosicao = new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
            NotificarMudanca(novaPosicao);
        }
    }

    private void NotificarMudanca(Vector2 valor)
    {
        OnVector2Mudou?.Invoke(valor);
    }

}
