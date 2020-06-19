using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public PlayerProperties myProperties;
    
    private GameManager _gm;
    private Rigidbody2D _rb;

    private float _moveDir;
    
    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (myProperties.isDead)
            return;

        GetInput();
    }

    private void GetInput()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(new Vector2(0, myProperties.jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDir * myProperties.moveSpeed, _rb.velocity.y);
    }

    private void Die()
    {
        myProperties.isDead = true;
        _gm.KillPlayer(myProperties);
    }
}
