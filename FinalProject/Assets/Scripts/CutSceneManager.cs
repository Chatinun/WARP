using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string sceneName;
    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneName);
    }
}
