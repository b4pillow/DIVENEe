using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{
    [SerializeField] private LifeSystemPlayer vidaPlayer;
    [SerializeField] private LifeUI vidaCura;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (vidaPlayer.health < 12)
            {
                vidaPlayer.health++;
                vidaCura.ChangeBar(vidaPlayer.health, vidaPlayer.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}