using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 5f;

    public float MoveDistance = 10f;

    private Vector2 initalPosition;

    private int Direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        initalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Direction * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, initalPosition);

        if (distance >= MoveDistance)
        {
            Direction *= -1;
        }
    }
}