using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVector2 : MonoBehaviour
{
   public ObserverVector2 V2;

    private void OnEnable()
    {
        V2.OnVector2Mudou += ReagirMudanca;
    }

    private void OnDisable()
    {
        V2.OnVector2Mudou -= ReagirMudanca;
    }

    private void ReagirMudanca(Vector2 valor)
    {
        Debug.Log($"O valor Vector2 mudou para: {valor}");
    }

}
