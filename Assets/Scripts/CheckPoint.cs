using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    
    
    void Start()
    {
   
    }
 
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
               
            //col.gameObject.GetComponent<Player>().Position = transform.position;
            Destroy(gameObject, 1f);
        }
    }
}
