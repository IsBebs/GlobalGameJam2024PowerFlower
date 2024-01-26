using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D rigidbody2D;

    public void SetDirection(Vector2 newDirection)
    {
        rigidbody2D.velocity = newDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

}
