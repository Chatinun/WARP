using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int playerHealth;
    public int maxPlayerHealth = 5;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite healthFull;
    [SerializeField] private Sprite healthEmpty;

    public static HealthController instance;

    private void Awake()
    {
        instance = this;
        //PlayerPrefs.SetInt("PlayerHealth", 3);
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Floor1")
        {
            PlayerPrefs.SetInt("PlayerHealth", maxPlayerHealth);
        }
        playerHealth = PlayerPrefs.GetInt("PlayerHealth", playerHealth);

        UpdateHealth();
    }

    public void UpdateHealth()
    {
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);

        if (playerHealth >= maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if(playerHealth <= 0)
        {
            playerHealth = 0;
        }

        if (playerHealth <= 0)
        {
            StartCoroutine(PlayerController.instance.Die());
            StartCoroutine(ResetScene());
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                //hearts[i].color = Color.white;
                hearts[i].sprite = healthFull;
            }
            else
            {
                //hearts[i].color = Color.black;
                hearts[i].sprite = healthEmpty;
            }
        }

    }

    private void Update()
    {
        UpdateHealth();
    }

    public void DamageHealth(int damage)
    {
        playerHealth -= damage;
    }

    public void HealHealth(int heal)
    {
        playerHealth += heal;
    }

    IEnumerator ResetScene()
    {
        PlayerPrefs.SetInt("PlayerHealth", maxPlayerHealth);
        yield return new WaitForSeconds(2.5f);
        if (PlayerPrefs.GetInt("isHardcore") == 0)
        {
            LevelLoader.instance.LoadCustomLevel(SceneManager.GetActiveScene().buildIndex);
        }
        else if(PlayerPrefs.GetInt("isHardcore") == 1)
        {
            PlayerPrefs.DeleteKey("boucingBulletUnlock");
            PlayerPrefs.DeleteKey("speedBulletUnlock");
            PlayerPrefs.DeleteKey("homingBulletUnlock");
            PlayerPrefs.DeleteKey("LastWeapon");
            LevelLoader.instance.LoadCustomLevel(2);
        }
        
    }

}
