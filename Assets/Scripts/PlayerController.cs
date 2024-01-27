using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float force;
    [SerializeField]
    Rigidbody2D rigidbody2D;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("PlayerSprite");
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
        if (newVelocity.normalized * force * Time.fixedDeltaTime != new Vector2(0,0))
        {
            player.GetComponent<Animator>().SetFloat("Speed", force * Time.fixedDeltaTime);
        }
        rigidbody2D.AddForce(newVelocity.normalized * force * Time.fixedDeltaTime);

    }
}
