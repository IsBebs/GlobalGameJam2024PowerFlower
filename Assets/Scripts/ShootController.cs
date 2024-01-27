using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    BulletPool normalBulletPool;


    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirection = mousePos- transform.position ;
            GameObject bulletObject = normalBulletPool.GetNextBulletInPool();
            IBullet bulletInterface = bulletObject.GetComponent<IBullet>();
            bulletInterface.SetNewBulletValues(lookDirection.normalized, transform.position);
        }
    }
}
