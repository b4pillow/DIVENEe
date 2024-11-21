using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{ 
    public int scoreValue;
    //private AudioSource Sound;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            //Sound.Play();
            scoreValue ++;
            GameController.Instace.UpdateScore(scoreValue);
            Destroy(gameObject, 0.1f);
        }
    }

    void Start()
    {
        //Sound = GetComponent<AudioSource>();
    }
}