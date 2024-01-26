using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float force;
    [SerializeField]
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {

        Vector2 newVelocity = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            newVelocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newVelocity.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            newVelocity.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newVelocity.y -= 1;
        }
        rigidbody2D.AddForce(newVelocity.normalized * force * Time.fixedDeltaTime);

    }
}
