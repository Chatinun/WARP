using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPotion : MonoBehaviour
{
    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(particle, transform.position, transform.rotation);
            PlayerPrefs.SetInt("homingBulletUnlock", 1);
            AudioManager.instance.audioPlay("PotionPickup");
            //WeaponManager.instance.homingBulletUnlock = true;
            WeaponManager.instance.SetWeapon(3);
            WeaponManager.instance.weaponID = 3;
            WeaponManager.instance.weaponCycle = 3;
        }
    }
}
