using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucingPotion : MonoBehaviour
{
    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(particle, transform.position, transform.rotation);
            PlayerPrefs.SetInt("boucingBulletUnlock", 1);
            AudioManager.instance.audioPlay("PotionPickup");
            //WeaponManager.instance.boucingBulletUnlock = true;
            WeaponManager.instance.SetWeapon(4);
            WeaponManager.instance.weaponID = 4;
            WeaponManager.instance.weaponCycle = 4;
        }
    }
}
