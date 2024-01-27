using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy, IEnemy, IDamage
{
    [SerializeField] GameObject startLook;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    private float _timer;
    // Start is called before the first frame update

    BulletPool bulletPool;
    GameObject playerObject;
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
        Alerted();
    }

    public void Alerted()
    {
        _isAlerted = true;

        Vector3 playerPos = player.transform.position;
        Vector2 lookDirection = transform.position - playerPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Start countdown fire
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
           
            IBullet bulletInterface = bulletPool.GetNextBulletInPool().GetComponent<IBullet>();
            bulletInterface.SetNewBulletValues(lookDirection.normalized, transform.position);
            _timer = 1;
        }
    }

    public void Damage(int damage)
    {
        Debug.Log("Träff2");
    }
}
