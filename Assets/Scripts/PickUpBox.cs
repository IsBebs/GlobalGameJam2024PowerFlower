using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{

    [SerializeField]
    int PickUpAmmount;
    [SerializeField]
    PickupType pickupType = PickupType.Confeti;
    [SerializeField]
    string pickupSoundName;
    enum PickupType {Pie,Confeti,Health }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAmmo playerAmmo = collision.gameObject.GetComponent<PlayerAmmo>();
            IDamage playerHealth = collision.gameObject.GetComponent<IDamage>();

            GameObject playerObject = collision.gameObject;
            switch (pickupType)
            {
                case PickupType.Confeti:
                    playerAmmo.AddConfettiAmmo(PickUpAmmount);
                    break;
                case PickupType.Health:
                    playerHealth.Damage(-PickUpAmmount);
                    break;
                case PickupType.Pie:
                    playerAmmo.AddPieAmmo(PickUpAmmount);
                    break;
            }
            SoundManager.Instance.PlaySound(pickupSoundName);
            Destroy(this.gameObject);

        }
    }

    // Start is called before the first frame update
}
