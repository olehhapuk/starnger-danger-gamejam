using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatch : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            _audioSource.Play();
    }
}
