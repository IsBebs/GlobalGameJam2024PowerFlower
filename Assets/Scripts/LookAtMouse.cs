using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos= camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection =  transform.position - mousePos;
        float angleRad = Mathf.Atan2(lookDirection.x, -lookDirection.y);
        float angle = angleRad * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
