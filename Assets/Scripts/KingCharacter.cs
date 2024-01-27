using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingCharacter : MonoBehaviour, IDamage
{
    [SerializeField] int sceneBuildIndex;

    public void Damage(int damage)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    

    
}
