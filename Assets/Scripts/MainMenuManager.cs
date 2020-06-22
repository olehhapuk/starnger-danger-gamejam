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
        if (_audioManager != null)
            _audioManager.PlayAudio(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
