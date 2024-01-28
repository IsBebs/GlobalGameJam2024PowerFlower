using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemey : Enemy, IEnemy
{
    
    
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField] float _timer;
    private float _currentTime;

    [SerializeReference]
    float distance;
    [SerializeReference]
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _isAlerted = true;
        Vector3 startLookPos = startLook.transform.position;
        Vector2 lookDirection = transform.position - startLookPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _currentTime = 1;
    }

    public void Update()
    {
        _currentTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isAlerted)
        {
            Vector3 playerPos = player.transform.position;
            Vector2 lookDirection = playerPos - transform.position;
            float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
            float angle = angleRad * (180 / Mathf.PI);
            transform.rotation = Quaternion.Euler(0, 0, 180+angle);

            rigidbody2D.AddForce(lookDirection.normalized * force * Time.fixedDeltaTime);
        }

        Vector2 direction = player.transform.position - gameObject.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, distance, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Alerted();
            }
        }
      
    }

    public void Alerted()
    {
        _isAlerted = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _currentTime <= 0)
        {
            player.GetComponent<IDamage>().Damage(damage);
            _currentTime = _timer;
        }
    }

}
