using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;
    [SerializeField] private Transform spawner;

    private PlayerController _currentPlayer;
    
    private void Start()
    {
        SpawnNewPlayer();
    }

    public void ChangePlayer()
    {
        players.RemoveAt(0);
        if (players.Count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        SpawnNewPlayer();
    }

    private void SpawnNewPlayer()
    {
        _currentPlayer = players[0].GetComponent<PlayerController>();
        Instantiate(_currentPlayer.gameObject, spawner.position, Quaternion.identity);
    }
}
