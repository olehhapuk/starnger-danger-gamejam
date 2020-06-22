using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public List<Audio> Audios;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string audioName)
    {
        foreach (var audio in Audios)
        {
            if (audio.Name == audioName)
            {
                _audioSource.clip = audio.AudioClip;
                _audioSource.Play();
            }
        }
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }
}
