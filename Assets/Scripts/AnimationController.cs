using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void playershootAnimationDone(ShootController shoot)
    {
        shoot.animationDone = true;
    }
}
