using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventStuff : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void triggerValue(string variableName)
    {
        animator.SetTrigger(variableName);
    }

    public void speedValue(string variableName, float speedValue)
    {
        animator.SetFloat(variableName, speedValue);
        
    }
    
}
