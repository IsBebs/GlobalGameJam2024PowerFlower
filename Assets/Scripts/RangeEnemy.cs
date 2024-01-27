using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy, IEnemy
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeReference]
    LayerMask layerMask;
    private float _timer;
    // Start is called before the first frame update
    [SerializeField]
    BulletPool bulletPool;
    [SerializeReference]
    float distance;
    
    void Start()
    {
        GameObject bulletPoolObject = GameObject.FindWithTag("StandingBulletPool");
        bulletPool = bulletPoolObject.GetComponent<BulletPool>();
        _timer = Random.Range(minTime,maxTime);

        Vector3 startLookPos = startLook.transform.position;
        Vector2 lookDirection = transform.position - startLookPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlerted)
        {
            Vector3 playerPos = player.transform.position;
            Vector2 lookDirection = transform.position - playerPos;
            float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
            float angle = angleRad * (180 / Mathf.PI);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            //Start countdown fire
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = Random.Range(minTime, maxTime);

                lookDirection = playerPos - transform.position;
                IBullet bulletInterface = bulletPool.GetNextBulletInPool().GetComponent<IBullet>();
                bulletInterface.SetNewBulletValues(lookDirection.normalized, transform.position);
                Debug.Log("Fire");
            }
        }
        
    }

    //
    public void FixedUpdate()
    {
        Vector2 direction = player.transform.position - gameObject.transform.position;

        RaycastHit2D hit =  Physics2D.Raycast(gameObject.transform.position, direction, distance, layerMask);
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

    



}
