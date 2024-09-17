using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFloat : MonoBehaviour
{
    public ObserverFloat F;

    private void OnEnable()
    {
        F.OnFloatMudou += ReagirMudanca;
    }

    private void OnDisable()
    {
        F.OnFloatMudou -= ReagirMudanca;
    }

    private void ReagirMudanca(float valor)
    {
        Debug.Log($"O valor float mudou para: {valor}");
    }

}
