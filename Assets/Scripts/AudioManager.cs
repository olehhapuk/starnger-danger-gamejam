using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Audio
{
    public string Name;
    public AudioClip AudioClip;
}

public class AudioManager : MonoBehaviour
{
    public string currentlyPlaying;
    
    [SerializeField] private List<Audio> audios;
    
    private AudioSource _audioSource;
    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string audioName)
    {
        foreach (var audio in audios)
        {
            if (audio.Name == audioName)
            {
                _audioSource.clip = audio.AudioClip;
                _audioSource.Play();
                currentlyPlaying = audioName;
            }
        }
    }
}
