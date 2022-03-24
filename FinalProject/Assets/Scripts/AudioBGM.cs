using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioBGM : MonoBehaviour
{
    public AudioClip[] audioClip;
    private AudioClip currentAudio;
    public AudioSource audioSource;

    private bool audioBGM1played;
    private bool audioBGM2played;
    private bool audioBGM3played;
    private bool audioBGM4played;

    public static AudioBGM instance = null;

    void Awake()
    {
        audioSource.Play();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    private void Update()
    {
        /*
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.clip = audioClip[4];
            if (!audioBGM4played)
            {
                audioSource.Play();
                audioBGM4played = true;
            }
            
        }
        else if (SceneManager.GetActiveScene().name == "Floor1" || SceneManager.GetActiveScene().name == "Floor2")
        {
            audioSource.clip = audioClip[0];
            if (!audioBGM1played)
            {
                audioSource.Play();
                audioBGM1played = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Floor4")
        {
            audioSource.clip = audioClip[1];
            if (!audioBGM2played)
            {
                audioSource.Play();
                audioBGM2played = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Floor7")
        {
            audioSource.clip = audioClip[2];
            if (!audioBGM3played)
            {
                audioSource.Play();
                audioBGM3played = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Floor10")
        {
            audioSource.clip = audioClip[3];
            if (!audioBGM4played)
            {
                audioSource.Play();
                audioBGM4played = true;
            }
        }
        else if(SceneManager.GetActiveScene().name == "EndCutScene")
        {
            audioSource.Stop();
        }
        */

        if (SceneManager.GetActiveScene().name == "EndCutScene")
        {
            audioSource.Stop();
        }
    }

    public void PlayMainMenu()
    {
        audioSource.clip = audioClip[4];
        if (!audioSource.isPlaying || currentAudio != audioClip[4])
        {
            audioSource.Play();
            currentAudio = audioClip[4];
        }
    }

    public void PlayBGM1()
    {
        audioSource.clip = audioClip[0];
        if (!audioSource.isPlaying || currentAudio != audioClip[0])
        {
            audioSource.Play();
            currentAudio = audioClip[0];
        }
    }

    public void PlayBGM2()
    {
        audioSource.clip = audioClip[1];
        if (!audioSource.isPlaying || currentAudio != audioClip[1])
        {
            audioSource.Play();
            currentAudio = audioClip[1];
        }
    }

    public void PlayBGM3()
    {
        audioSource.clip = audioClip[2];
        if (!audioSource.isPlaying || currentAudio != audioClip[2])
        {
            audioSource.Play();
            currentAudio = audioClip[2];
        }
    }

    public void PlayBossBGM()
    {
        audioSource.clip = audioClip[3];
        if (!audioSource.isPlaying || currentAudio != audioClip[2])
        {
            audioSource.Play();
            currentAudio = audioClip[3];
        }
    }

}
