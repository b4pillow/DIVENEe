using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverString : MonoBehaviour
{
    public event Action<string> OnStringMudou;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string novaString = "Mudança detectada!";
            NotificarMudanca(novaString);
        }
    }

    private void NotificarMudanca(string valor)
    {
        OnStringMudou?.Invoke(valor);
    }

}
