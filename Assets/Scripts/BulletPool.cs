using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    int bulletPoolSize;

    List<GameObject> bulletPool = new List<GameObject>();

    int poolIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);
            bulletPool.Add(bulletObject);
            bulletObject.SetActive(false);
        }
    }

    public GameObject GetNextBulletInPool()
    {
        GameObject currentBullet = bulletPool[poolIndex];
        poolIndex++;
        poolIndex = poolIndex % bulletPool.Count;
        currentBullet.SetActive(true);
        return currentBullet;
    }
}
