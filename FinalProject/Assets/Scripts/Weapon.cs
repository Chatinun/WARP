using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public KeyCode lastHitKey = KeyCode.RightArrow;

    public static Weapon instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastHitKey = KeyCode.RightArrow;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastHitKey = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastHitKey = KeyCode.UpArrow;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastHitKey = KeyCode.DownArrow;
        }

        //if (!PlayerController.instance.isJumping)
        //{
        //    ShootController();
        //}

        ShootController();
    }

    void ShootController()
    {

        if (timeBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.Z) && !PlayerController.instance.isDead)
            {
                if (lastHitKey == KeyCode.RightArrow)
                {
                    GameObject go = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    if (WeaponManager.instance.weaponID != 3)
                    {
                        go.GetComponent<Bullet>().direction = Vector3.right;
                        go.GetComponent<Bullet>().launchOffset = Vector3.zero;
                    }

                }
                else if (lastHitKey == KeyCode.LeftArrow)
                {
                    GameObject go = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    if (WeaponManager.instance.weaponID != 3)
                    {
                        go.GetComponent<Bullet>().direction = -Vector3.right;
                        go.GetComponent<Bullet>().launchOffset = Vector3.zero;
                    }

                }
                else if (lastHitKey == KeyCode.UpArrow)
                {
                    GameObject go = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    if (WeaponManager.instance.weaponID != 3)
                    {
                        go.GetComponent<Bullet>().direction = Vector3.up;
                        go.GetComponent<Bullet>().launchOffset = new Vector3(-0.1f, 0.7f);
                    }

                }
                else if (lastHitKey == KeyCode.DownArrow)
                {
                    GameObject go = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    if (WeaponManager.instance.weaponID != 3)
                    {
                        go.GetComponent<Bullet>().direction = -Vector3.up;
                        go.GetComponent<Bullet>().launchOffset = new Vector3(-0.1f, -0.7f);
                    }
                }

                if(WeaponManager.instance.weaponID == 1)
                {
                    AudioManager.instance.audioPlay("NormalBullet");
                }else if(WeaponManager.instance.weaponID == 2)
                {
                    AudioManager.instance.audioPlay("SpeedBullet");
                }else if(WeaponManager.instance.weaponID == 3)
                {
                    AudioManager.instance.audioPlay("HomingBullet");
                }else if(WeaponManager.instance.weaponID == 4)
                {
                    AudioManager.instance.audioPlay("BouncingBullet");
                }
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void SetBulletPrefab(GameObject newBullet)
    {
        bulletPrefab = newBullet;
    }
}
