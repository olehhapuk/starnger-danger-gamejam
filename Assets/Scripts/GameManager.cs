using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController[] players;
    
    private int _activePlayerIndex;
    
    private void Start()
    {
        players[0].isActive = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ChangeActivePlayer();
        }
    }

    private void ChangeActivePlayer()
    {
        // Set active player index
        if (_activePlayerIndex < players.Length - 1)
            _activePlayerIndex++;
        else
            _activePlayerIndex = 0;

        // Set player as active
        players[_activePlayerIndex].isActive = true;

        // Make other player unactive
        for (int i = 0; i < players.Length; i++)
        {
            if (i != _activePlayerIndex)
                players[i].isActive = false;
        }
    }
}
