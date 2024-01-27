using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemey : Enemy, IEnemy
{
    [SerializeField] GameObject startLook;
    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 playerPos = GameObject.FindWithTag("player").transform.position;
        Vector2 lookDirection = transform.position - playerPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
