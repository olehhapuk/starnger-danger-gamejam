using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string Name;
    public AudioClip AudioClip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Audio> audios;
    
    private static AudioManager _instance;
    private AudioSource _audioSource;

    private void Awake()
    {
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
            }
        }
    }
}
