using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private AudioManager _audioManager;
    
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        var activeScene = SceneManager.GetActiveScene().name;
        if (_audioManager != null)
        {
            if (_audioManager.currentlyPlaying != activeScene)
                _audioManager.PlayAudio(activeScene);
        }
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
