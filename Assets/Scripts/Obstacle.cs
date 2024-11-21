using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int requiredCoins = 25;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (player.totalScore >= requiredCoins)
            {
                player.SpendCoins(requiredCoins);
                Destroy(gameObject, 0.1f);
            }
        else
        {
            Debug.Log("Precisa de mais moedas");
        }
        }
    }
}
