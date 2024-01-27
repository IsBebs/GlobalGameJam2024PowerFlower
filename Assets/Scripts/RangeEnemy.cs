using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy, IEnemy
{
    [SerializeField] GameObject startLook;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;

    private float _timer;
    // Start is called before the first frame update

    void Start()
    {
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

        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        Vector2 lookDirection = transform.position - playerPos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Start countdown fire
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Debug.Log("Fire");
            _timer = 1;
        }
    }
}
