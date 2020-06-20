using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerProperties myProperties;
    public ActivePlayer activePlayer;
    public Text playerName;


    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform shootLocation;
    
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
        Flip();
    }

    private void Flip()
    {
        if (_moveDir < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else if (_moveDir > 0)
            transform.eulerAngles = new Vector2(0, 0);
    }

    private void GetInput()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(new Vector2(0, myProperties.jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            switch (activePlayer)
            {
                case ActivePlayer.Explosion:
                    Instantiate(explosionPrefab, shootLocation.position, transform.rotation);
                    break;
                case ActivePlayer.Gravity:
                    print("Flipping gravity!");
                    break;
                case ActivePlayer.Slime:
                    print("Spawning slimes!");
                    break;
                case ActivePlayer.Teleport:
                    print("Creating teleports!");
                    break;
                default:
                    break;
            }
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
