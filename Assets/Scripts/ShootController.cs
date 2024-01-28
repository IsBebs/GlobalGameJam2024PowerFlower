using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    BulletPool PieBulletPool;
    [SerializeField]
    BulletPool ConfetiGunBulletPool;
    [SerializeField]
    PlayerAmmo playerAmmo;
    Weapons currentWeapons = Weapons.Pie;
    [SerializeField]
    float soundRadius;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    AnimationController animationController;
    bool IsPlayingAnimation = false;
    //Mostly for animation
    GameObject player;
    enum Weapons {ConfettiGun,Pie }

    public void Awake()
    {
        player = GameObject.FindWithTag("PlayerSprite");
    }

    public void CreateShootSound()
    {
      Collider2D [] colliders =  Physics2D.OverlapCircleAll(transform.position, soundRadius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject colliderObject = colliders[i].gameObject;
            IEnemy enemyInterface = colliderObject.gameObject.GetComponent<IEnemy>();
            if(enemyInterface != null)
            {
                enemyInterface.Alerted();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWeapons == Weapons.Pie && playerAmmo.PieAmmo <= 0 && playerAmmo.ConfettiAmmo > 0)
        {
            currentWeapons = Weapons.ConfettiGun;
            playerAmmo.UpdateAmmoUiWithConfettiAmmo();
        }
        else if (currentWeapons == Weapons.ConfettiGun && playerAmmo.ConfettiAmmo <= 0 && playerAmmo.PieAmmo > 0)
        {
            currentWeapons = Weapons.Pie;
            playerAmmo.UpdateAmmoUiWithPieAmmo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pie");
            currentWeapons = Weapons.Pie;
            playerAmmo.UpdateAmmoUiWithPieAmmo();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Confeti");
            currentWeapons = Weapons.ConfettiGun;
            playerAmmo.UpdateAmmoUiWithConfettiAmmo();
        }

        if (Input.GetMouseButtonDown(0) && !IsPlayingAnimation)
        {
            if (playerAmmo.ConfettiAmmo > 0)
            {
                player.GetComponent<Animator>().SetTrigger("ConfettiShoot");
                IsPlayingAnimation = true;

            }
            else if (playerAmmo.PieAmmo > 0)
            {
                player.GetComponent<Animator>().SetTrigger("PieShoot");
                IsPlayingAnimation = true;
            }
            
        }
    }

    public void ShootPie()
    {
        ShootBullet(PieBulletPool);
        playerAmmo.UsePieAmmo();
        Debug.Log($"Pie Ammo{playerAmmo.PieAmmo}");
        playerAmmo.UpdateAmmoUiWithPieAmmo();
        IsPlayingAnimation = false;
    }

    public void ShootConfeti()
    {
        ShootBullet(ConfetiGunBulletPool);
        playerAmmo.UseConfettiAmmo();
        Debug.Log($"Confetti Ammo{playerAmmo.ConfettiAmmo}");
        IsPlayingAnimation  = false;
        playerAmmo.UpdateAmmoUiWithConfettiAmmo();
    }

    void ShootBullet(BulletPool bulletPool)
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - transform.position;
        GameObject bulletObject = bulletPool.GetNextBulletInPool();
        IBullet bulletInterface = bulletObject.GetComponent<IBullet>();
        bulletInterface.SetNewBulletValues(lookDirection.normalized, transform.position);
        CreateShootSound();
    }
}
