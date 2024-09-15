using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    public float speed;
    public float moveTime;
    private bool dirRight = true;
    private float timer;
    [SerializeField] private LifeSystemPlayer vidaPlayer;
    [SerializeField] private LifeUI vidaCura;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        timer += Time.deltaTime;
    
        if(timer >= moveTime)
        {
            dirRight = !dirRight;
            timer = 0f;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            vidaPlayer.Damage(1);
            vidaCura.ChangeBar(vidaPlayer.health, vidaPlayer.maxHealth);
        }
    }
}