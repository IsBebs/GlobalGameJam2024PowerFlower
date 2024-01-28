using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool AnimationDone { get; set; }

    public void PlayerShootAnimationDone()
    {
        AnimationDone = true;
    }
}
