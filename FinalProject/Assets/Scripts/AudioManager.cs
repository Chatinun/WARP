using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audio;
    [SerializeField] public AudioSource audioSource;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        */
    }

    public void audioPlay(string soundname)
    {
        switch (soundname)
        {
            case "Jump":
                audioSource.PlayOneShot(audio[0]);
                break;
            case "Dash":
                audioSource.PlayOneShot(audio[1]);
                break;
            case "Hurt":
                audioSource.PlayOneShot(audio[2]);
                break;
            case "NormalBullet":
                audioSource.PlayOneShot(audio[3]);
                break;
            case "SpeedBullet":
                audioSource.PlayOneShot(audio[4]);
                break;
            case "Dead":
                audioSource.PlayOneShot(audio[5]);
                break;
            case "EnemyDead":
                audioSource.PlayOneShot(audio[6]);
                break;
            case "Switch":
                audioSource.PlayOneShot(audio[7]);
                break;
            case "Diamond":
                audioSource.PlayOneShot(audio[8]);
                break;
            case "HomingBullet":
                audioSource.PlayOneShot(audio[9]);
                break;
            case "BouncingBullet":
                audioSource.PlayOneShot(audio[10]);
                break;
            case "BullEnrage":
                audioSource.PlayOneShot(audio[11]);
                break;
            case "ChangeWeapon":
                audioSource.PlayOneShot(audio[12]);
                break;
            case "DoorOpen":
                audioSource.PlayOneShot(audio[13]);
                break;
            case "Laser":
                audioSource.PlayOneShot(audio[14]);
                break;
            case "PotionPickup":
                audioSource.PlayOneShot(audio[15]);
                break;
            case "EnemyShoot":
                audioSource.PlayOneShot(audio[16]);
                break;
            case "FallingSpike":
                audioSource.PlayOneShot(audio[17]);
                break;
            case "FrogHitGround":
                audioSource.PlayOneShot(audio[18]);
                break;
            case "RockFall":
                audioSource.PlayOneShot(audio[19]);
                break;
            case "Saw":
                audioSource.PlayOneShot(audio[20]);
                break;
            case "BullAttack":
                audioSource.PlayOneShot(audio[21]);
                break;
            case "MouseHover":
                audioSource.PlayOneShot(audio[22]);
                break;
            case "Error":
                audioSource.PlayOneShot(audio[23]);
                break;
        }

    }
}
