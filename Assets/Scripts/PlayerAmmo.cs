using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI AmmoUI;
    [SerializeField]
    int ConfettiMaxAmmo;
    public int ConfettiAmmo { get { return confettiAmmo; } }
    int confettiAmmo = 0;
    [SerializeField]
    int PieMaxAmmo;
    public int PieAmmo { get { return pieAmmo; } }
    int pieAmmo;

    public void Start()
    {
        UpdateAmmoUiWithPieAmmo();
    }

    public void AddPieAmmo(int Ammo)
    {
        pieAmmo += Ammo;
        if (pieAmmo >= PieMaxAmmo)
        {
            pieAmmo = PieMaxAmmo;
        }
        UpdateAmmoUiWithPieAmmo();
    }

    public void UsePieAmmo()
    {
        pieAmmo--;
        UpdateAmmoUiWithPieAmmo();
    }


    public void AddConfettiAmmo(int Ammo)
    {
        confettiAmmo += Ammo;
        if (confettiAmmo > ConfettiMaxAmmo)
        {
            confettiAmmo = ConfettiMaxAmmo;
        }
        UpdateAmmoUiWithConfettiAmmo();
    }

    public void UseConfettiAmmo()
    {
        confettiAmmo--;
        UpdateAmmoUiWithConfettiAmmo();
    }

    public void UpdateAmmoUiWithPieAmmo()
    {
        AmmoUI.text = $"Pies:{pieAmmo}";
    }

    public void UpdateAmmoUiWithConfettiAmmo()
    {
        AmmoUI.text = $"Confetti:{confettiAmmo}";
    }

}
