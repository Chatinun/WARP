using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    public GameObject particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(particle, transform.position, transform.rotation);
            PlayerPrefs.SetInt("speedBulletUnlock", 1);
            AudioManager.instance.audioPlay("PotionPickup");
            //WeaponManager.instance.speedBulletUnlock = true;
            WeaponManager.instance.SetWeapon(2);
            WeaponManager.instance.weaponID = 2;
            WeaponManager.instance.weaponCycle = 2;
            Destroy(gameObject);
        }
    }
}
