using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventStuff : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void trigger(string variableName)
    {
        animator.SetTrigger(variableName);
    }
    
}
