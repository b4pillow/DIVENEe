using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerString : MonoBehaviour
{
     public ObserverString S;

    public void OnEnable()
    {
        S.OnStringMudou += ReagirMudanca;
    }

    public void OnDisable()
    {
        S.OnStringMudou -= ReagirMudanca;
    }

    private void ReagirMudanca(string valor)
    {
        Debug.Log($"A string mudou para: {valor}");
    }

}
