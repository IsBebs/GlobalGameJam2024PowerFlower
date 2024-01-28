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

    //Mostly for animation
    GameObject player;
    AnimationStates fireAnimation = AnimationStates.inactive;
    enum AnimationStates { inactive, activePie, activeConfetti, donePie, doneConfetti}
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

        if (Input.GetMouseButtonDown(0) && fireAnimation == AnimationStates.inactive)
        {
            if (playerAmmo.ConfettiAmmo > 0)
            {
                player.GetComponent<Animator>().SetTrigger("ConfettiShoot");
                fireAnimation = AnimationStates.activeConfetti;
            }
            else if (playerAmmo.PieAmmo > 0)
            {
                player.GetComponent<Animator>().SetTrigger("PieShoot");
                fireAnimation = AnimationStates.activePie;
            }
        }

        if (animationController.AnimationDone)
        {
            if (fireAnimation == AnimationStates.activeConfetti)
            {
                fireAnimation = AnimationStates.doneConfetti;
                animationController.AnimationDone = false;
            }
            else if (fireAnimation == AnimationStates.activePie)
            {
                fireAnimation = AnimationStates.donePie;
                animationController.AnimationDone = false;
            }
        }

        if (fireAnimation == AnimationStates.donePie)
        {
            ShootBullet(PieBulletPool);
            playerAmmo.UsePieAmmo();
            Debug.Log($"Pie Ammo{playerAmmo.PieAmmo}");
            fireAnimation = AnimationStates.inactive;

            playerAmmo.UpdateAmmoUiWithPieAmmo();
        }
        else if (fireAnimation == AnimationStates.doneConfetti)
        {
            ShootBullet(ConfetiGunBulletPool);
            playerAmmo.UseConfettiAmmo();
            Debug.Log($"Confeti Ammo{playerAmmo.ConfettiAmmo}");

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
}
