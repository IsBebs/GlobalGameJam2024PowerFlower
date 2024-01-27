using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunBullet : MonoBehaviour, IBullet
{
    [SerializeField]
    float activeTime;
    float activeTimer;
    [SerializeField]
    Collider2D shootCoolider;
    [SerializeField]
    int damage;
    [SerializeField]
    ContactFilter2D contactFilter;
    [SerializeField]
    Transform childTransform;
    [SerializeField]
    float offset;

    public void SetNewBulletValues(Vector2 newDirection, Vector3 newPosition)
    {
       
        activeTimer = activeTime;
        Vector3 offsetVector = new Vector2 (newDirection.x * offset, newDirection.y * offset);
        transform.position = newPosition + offsetVector;
        List<Collider2D> collisionResults = new List<Collider2D>();
        Physics2D.OverlapCollider(shootCoolider,contactFilter,collisionResults);
        float angleRad = Mathf.Atan2(newDirection.x, -newDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        childTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (collisionResults.Count > 0)
        {
            for (int i = 0; i < collisionResults.Count; i++)
            {
                GameObject collisionObject = collisionResults[i].gameObject;
                IDamage damageInterface = collisionObject.GetComponent<IDamage>();
                if (damageInterface != null)
                {
                    damageInterface.Damage(damage);
                }
            }

            //activate particle system
        }
    }

    private void Update()
    {
        activeTimer -= Time.deltaTime;
        if (activeTimer < 0)
        {
            gameObject.SetActive(false);
        }
    }

}
