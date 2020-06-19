using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PlayerProperties myProperties;
    
    private GameManager _gm;
    
    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (myProperties.isDead)
            return;
        
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * myProperties.moveSpeed * Time.deltaTime, 0));

        if (Input.GetButtonDown("Fire2"))
        {
            Die();
        }
    }

    private void Die()
    {
        myProperties.isDead = true;
        _gm.KillPlayer(myProperties);
    }
}
