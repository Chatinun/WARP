using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;

    public int weaponID;

    public bool speedBulletUnlock;
    public bool homingBulletUnlock;
    public bool boucingBulletUnlock;

    public static WeaponManager instance;

    public int weaponCycle = 1;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        weaponID = 1;

        if (!PlayerPrefs.HasKey("LastWeapon"))
        {
            weaponID = 1;
            weaponCycle = 1;
        }
        else
        {
            weaponID = PlayerPrefs.GetInt("LastWeapon");
            weaponCycle = PlayerPrefs.GetInt("LastWeapon");
        }
        
    }

    void Update()
    {
        WeaponCycle();
    }

    void WeaponCycle()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (PlayerPrefs.HasKey("boucingBulletUnlock"))
            {
                AudioManager.instance.audioPlay("ChangeWeapon");
                if (weaponCycle == 4)
                {
                    weaponCycle = 1;
                }
                else
                {
                    weaponCycle++;
                }
            }
            else if (PlayerPrefs.HasKey("homingBulletUnlock"))
            {
                AudioManager.instance.audioPlay("ChangeWeapon");
                if (weaponCycle == 3)
                {
                    weaponCycle = 1;
                }
                else
                {
                    weaponCycle++;
                }
            }
            else if (PlayerPrefs.HasKey("speedBulletUnlock"))
            {
                AudioManager.instance.audioPlay("ChangeWeapon");
                if (weaponCycle == 2)
                {
                    weaponCycle = 1;
                }
                else
                {
                    weaponCycle++;
                }
            }
        }

        switch (weaponCycle)
        {
            case 1:
                SetWeapon(1);
                weaponID = 1;
                break;
            case 2:
                SetWeapon(2);
                weaponID = 2;
                break;
            case 3:
                SetWeapon(3);
                weaponID = 3;
                break;
            case 4:
                SetWeapon(4);
                weaponID = 4;
                break;
        }

        PlayerPrefs.SetInt("LastWeapon", weaponID);

        /*
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponID != 1)
        {
            SetWeapon(1);
            weaponID = 1;
            AudioManager.instance.audioPlay("ChangeWeapon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponID != 2)
        {
            if (PlayerPrefs.HasKey("speedBulletUnlock"))
            {
                SetWeapon(2);
                weaponID = 2;
                AudioManager.instance.audioPlay("ChangeWeapon");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponID != 3)
        {
            if (PlayerPrefs.HasKey("homingBulletUnlock"))
            {
                SetWeapon(3);
                weaponID = 3;
                AudioManager.instance.audioPlay("ChangeWeapon");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && weaponID != 4)
        {
            if (PlayerPrefs.HasKey("boucingBulletUnlock"))
            {
                SetWeapon(4);
                weaponID = 4;
                AudioManager.instance.audioPlay("ChangeWeapon");
            }
        }
        */
    }

    public void SetWeapon(int weaponID)
    {
        switch (weaponID)
        {
            case 1:
                Weapon.instance.SetBulletPrefab(bullet[0]);
                Weapon.instance.startTimeBtwShots = 0.25f;
                break;
            case 2:
                Weapon.instance.SetBulletPrefab(bullet[1]);
                Weapon.instance.startTimeBtwShots = 0.1f;
                break;
            case 3:
                Weapon.instance.SetBulletPrefab(bullet[2]);
                Weapon.instance.startTimeBtwShots = 0.25f;
                break;
            case 4:
                Weapon.instance.SetBulletPrefab(bullet[3]);
                Weapon.instance.startTimeBtwShots = 0.5f;
                break;
        }
    }
}
