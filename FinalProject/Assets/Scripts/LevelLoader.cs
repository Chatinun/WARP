using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public GameObject noSavedMenu = null;

    public static LevelLoader instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioBGM.instance.PlayMainMenu();
        }
        else if (SceneManager.GetActiveScene().name == "Floor1" || SceneManager.GetActiveScene().name == "Floor2" || SceneManager.GetActiveScene().name == "Floor3")
        {
            AudioBGM.instance.PlayBGM1();
        }
        else if (SceneManager.GetActiveScene().name == "Floor4" || SceneManager.GetActiveScene().name == "Floor5" || SceneManager.GetActiveScene().name == "Floor6")
        {
            AudioBGM.instance.PlayBGM2();
        }
        else if (SceneManager.GetActiveScene().name == "Floor7" || SceneManager.GetActiveScene().name == "Floor8" || SceneManager.GetActiveScene().name == "Floor9")
        {
            AudioBGM.instance.PlayBGM3();
        }
        else if (SceneManager.GetActiveScene().name == "Floor10")
        {
            AudioBGM.instance.PlayBossBGM();
        }
        else if (SceneManager.GetActiveScene().name == "EndCutScene")
        {
            AudioBGM.instance.audioSource.Stop();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadCustomLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HardcoreSelected()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("isHardcore", 1);
    }

    public void NormalSelected()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("isHardcore", 0);
    }

    public void LoadSavedLevel()
    {
        if (PlayerPrefs.HasKey("SavedSceneIndex"))
        {
            StartCoroutine(LoadLevel(PlayerPrefs.GetInt("SavedSceneIndex")));
        }
        else
        {
            AudioManager.instance.audioPlay("Error");
            noSavedMenu.SetActive(true);
        }
        
    }
    
}
