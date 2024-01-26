using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletPoolSize;
    List<Bullet> bulletPool = new List<Bullet>();
    [SerializeField]
    Transform bulletPoolParent;

    int poolIndex =0;
    private void Start()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity,bulletPoolParent);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bulletPool.Add(bullet);
            bulletObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet currentBullet = bulletPool[poolIndex];
            currentBullet.gameObject.SetActive(true);
            currentBullet.transform.position = transform.position;
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirection = mousePos- transform.position ;
            currentBullet.SetDirection(lookDirection.normalized);
            poolIndex++;
            poolIndex = poolIndex % bulletPool.Count;
        }
    }
}
