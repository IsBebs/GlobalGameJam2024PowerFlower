using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{

    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D rigidbody2D;
    [SerializeField]
    int Damage;


    public void SetNewBulletValues(Vector2 newDirection, Vector3 position)
    {
        rigidbody2D.velocity = newDirection * speed;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;
        IDamage damageInterface = colliderObject.GetComponent<IDamage>();
        if (damageInterface != null)
        {
            damageInterface.Damage(Damage);
        }
        gameObject.SetActive(false);
    }

}
