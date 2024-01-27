using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected bool _isAlerted = false;

    protected GameObject player;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

}
