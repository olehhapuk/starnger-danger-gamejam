using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerProperties[] players;

    private void Start()
    {
        ChangePlayer(0);
        if (SceneManager.GetActiveScene().name == "MainMenu")
            FindObjectOfType<AudioManager>().PlayAudio(SceneManager.GetActiveScene().name);
        else
            FindObjectOfType<AudioManager>().PlayAudio("01_Lvl");
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
            print("Can't do that");
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
        player.playerName.text = players[index].name;
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
