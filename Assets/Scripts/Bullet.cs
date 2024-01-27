using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{

    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D rigidbody2D;


    public void SetNewBulletValues(Vector2 newDirection, Vector3 position)
    {
        rigidbody2D.velocity = newDirection * speed;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

}
