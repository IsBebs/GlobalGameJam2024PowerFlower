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
    GameObject player;

    enum Weapons {ConfettiGun,Pie }

    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
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

        if (Input.GetMouseButtonDown(0))
        {

            switch (currentWeapons)
            {
                case Weapons.ConfettiGun:

                    if (playerAmmo.ConfettiAmmo > 0)
                    {
                        ShootBullet(ConfetiGunBulletPool);
                        playerAmmo.UseConfettiAmmo();
                        player.GetComponent<Animator>().SetTrigger("ConfettiShoot");
                        Debug.Log($"Confeti Ammo{playerAmmo.ConfettiAmmo}");
                    }
                    
                    break;

                case Weapons.Pie:

                    if (playerAmmo.PieAmmo > 0)
                    {
                        ShootBullet(PieBulletPool);
                        playerAmmo.UsePieAmmo();
                        player.GetComponent<Animator>().SetTrigger("PieShoot");
                        Debug.Log($"Pie Ammo{playerAmmo.PieAmmo}");
                    }
                    break;
            }

            switch (currentWeapons)
            {
                case Weapons.ConfettiGun:
                    playerAmmo.UpdateAmmoUiWithConfettiAmmo();
                    break;

                case Weapons.Pie:
                    playerAmmo.UpdateAmmoUiWithPieAmmo();
                    break;
            }
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
}
