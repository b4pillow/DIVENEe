using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    
   private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
        {
    
            col.GetComponent<PlayerController>().SaveCheckpoint(transform.position);
            gameObject.SetActive(false);
            //Destroy(gameObject, 1f);
        }
    }
}


