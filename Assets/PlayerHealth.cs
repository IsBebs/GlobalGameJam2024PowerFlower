using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamage
{

    int Hp;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    TextMeshProUGUI HpUI;

    public void Damage(int damage)
    {
        Hp -= damage;
        if (Hp > maxHealth)
        {
            Hp = maxHealth;
        }

        if (Hp < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        HpUI.text = $"HP:{Hp}";
    }

    // Start is called before the first frame update
    void Start()
    {
        Hp = maxHealth;
        HpUI.text = $"HP:{Hp}";
    }
}
