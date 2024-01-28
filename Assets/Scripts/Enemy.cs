using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{

    protected bool _isAlerted = false;

    protected GameObject player;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected GameObject deadPreFab;
    [SerializeField]protected float force;
    [SerializeField]protected GameObject startLook;
    [SerializeField] protected int damage;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    protected void Dead()
    {
        GameObject dead = GameObject.Instantiate(deadPreFab);
        dead.transform.position = gameObject.transform.position;
        SoundManager.Instance.PlaySound("EnemyDead");
        Destroy(this.gameObject, 0.01f);
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("död");
            Dead();
        }
    }

  



}
