using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventStuff : MonoBehaviour
{
    [SerializeField] Animator animator;

    GameObject player;

    Vector2 speed;

    [SerializeField] string speedName;


    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        speed = player.GetComponent<Rigidbody2D>().velocity;
    }

    public void triggerValue(string variableName)
    {
        animator.SetTrigger(variableName);
    }

    public void Update()
    {
        if (speed.x > 0 || speed.y > 0)
        {
            animator.SetFloat(speedName, Mathf.Sqrt(speed.x * speed.x + speed.y * speed.y));
        }
    }

}
