using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public float speed;

    public bool do_a_flip;

    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void DoTheFlip()
    {
        if (do_a_flip)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }

    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && col.CompareTag("Wall"))
        {
            do_a_flip = !do_a_flip;
        }
        DoTheFlip(); // Atualiza a rotação do inimigo
    }
}
