using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject weaponUI;
    public GameObject pausePanel;

    public Sprite[] weaponUIImage;

    private void Start()
    {
        pausePanel.SetActive(false);
        PlayerPrefs.SetInt("SavedSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        UpdateWeaponUI();
        PauseMenu();
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.audioPlay("MouseHover");
            if (!pausePanel.active)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ResumeMenu()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        LevelLoader.instance.LoadCustomLevel(1);
    }

    void UpdateWeaponUI()
    {
        if (WeaponManager.instance.weaponID == 1)
        {
            weaponUI.GetComponent<Image>().sprite = weaponUIImage[0];
        }
        else if (WeaponManager.instance.weaponID == 2)
        {
            weaponUI.GetComponent<Image>().sprite = weaponUIImage[1];
        }
        else if (WeaponManager.instance.weaponID == 3)
        {
            weaponUI.GetComponent<Image>().sprite = weaponUIImage[2];
        }
        else if (WeaponManager.instance.weaponID == 4)
        {
            weaponUI.GetComponent<Image>().sprite = weaponUIImage[3];
        }
    }

}
