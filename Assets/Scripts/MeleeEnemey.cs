using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemey : Enemy, IEnemy
{
    
    
    [SerializeField]
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _isAlerted = true;
        Vector3 startLookPos = startLook.transform.position;
        Vector2 lookDirection = transform.position - startLookPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Alerted();
    }

    public void Alerted()
    {
        
        Vector3 playerPos = player.transform.position;
        Vector2 lookDirection = playerPos - transform.position;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rigidbody2D.AddForce(lookDirection.normalized * force * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Krock");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Krock1");
            player.GetComponent<IDamage>().Damage(damage);
        }
    }

}
