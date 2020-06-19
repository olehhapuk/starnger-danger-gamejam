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
    }

    private void Update()
    {
        if (Input.GetButtonDown("1"))
            ChangePlayer(0);
        else if (Input.GetButtonDown("2"))
            ChangePlayer(1);
    }

    private void ChangePlayer(int index)
    {
        if (players[index].isDead)
        {
            print("Can't do that");
            return;
        }
        player.myProperties = players[index];
        player.GetComponent<SpriteRenderer>().sprite = players[index].sprite;
        player.GetComponentInChildren<Text>().text = players[index].name;
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
