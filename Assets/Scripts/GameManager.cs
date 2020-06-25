using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerProperties[] players;
    [SerializeField] private List<Sprite> playerIcons;
    [SerializeField] private List<Sprite> playerActiveIcons;
    [SerializeField] private List<Sprite> playerDeadIcons;
    [SerializeField] private List<Image> playerImages;
    [SerializeField] private List<AudioClip> audios;

    private AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChangePlayer(0);
        var activeScene = SceneManager.GetActiveScene().name;
        
        if (activeScene == "MainMenu" || activeScene == "01_Lvl" || activeScene == "02_Lvl")
        {
            if (_audioManager.currentlyPlaying != activeScene)
                _audioManager.PlayAudio(activeScene);
        }
        else
        {
            var index = Random.Range(1, 3);
            var audioToPlay = "0" + index + "_Lvl";
            if (_audioManager.currentlyPlaying != audioToPlay)
                _audioManager.PlayAudio(audioToPlay);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("1"))
            ChangePlayer(0);
        else if (Input.GetButtonDown("2"))
            ChangePlayer(1);
        else if (Input.GetButtonDown("3"))
            ChangePlayer(2);
        else if (Input.GetButtonDown("4"))
            ChangePlayer(3);
    }

    private void ChangePlayer(int index)
    {
        if (players[index].isDead)
        {
            _audioSource.clip = audios[0];
            _audioSource.Play();
            return;
        }
        player.myProperties = players[index];
        ActivePlayer activePlayer;
        switch (index)
        {
            case 0:
                activePlayer = ActivePlayer.Explosion;
                break;
            case 1:
                activePlayer = ActivePlayer.Gravity;
                break;
            case 2:
                activePlayer = ActivePlayer.Teleport;
                break;
            case 3:
                activePlayer = ActivePlayer.Slime;
                break;
            default:
                return;
        }

        player.activePlayer = activePlayer;
        player.GetComponent<Animator>().SetFloat("ActiveCharacter", index);
        player.GetComponent<SpriteRenderer>().sprite = players[index].sprite;
        _audioSource.clip = audios[1];
        _audioSource.Play();
        for (int i = 0; i < players.Length; i++)
        {
            if (i == index)
                playerImages[i].sprite = playerActiveIcons[i];
            else
            {
                if (players[i].isDead)
                    playerImages[i].sprite = playerDeadIcons[i];
                else
                    playerImages[i].sprite = playerIcons[i];
            }
        }
    }

    public void KillPlayer(PlayerProperties newProperties)
    {
        var changedPlayer = false;
        
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == newProperties.name)
            {
                players[i] = newProperties;
            }

            if (!players[i].isDead)
            {
                ChangePlayer(i);
                changedPlayer = true;
            }
        }

        if (!changedPlayer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
