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
    public Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("Slime")]
    [SerializeField] private GameObject slime;
    [SerializeField] private Transform slimePlaceholder;
    
    private GameManager _gm;
    private Rigidbody2D _rb;

    private float _moveDir;
    private bool _canMove = true;
    private bool _isGrounded;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (myProperties.isDead)
            return;

        CheckGround();

        if (_canMove)
            GetInput();
        else _moveDir = 0;
        
        Flip();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDir * myProperties.moveSpeed, _rb.velocity.y);
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _isGrounded ? Color.red : Color.green;
        
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
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
        
        animator.SetFloat("Speed", Mathf.Abs(_moveDir));

        if (Input.GetButtonDown("Jump") && _isGrounded)
            _rb.AddForce(new Vector2(0, myProperties.jumpForce), ForceMode2D.Impulse);

        if (Input.GetButtonDown("Fire1"))
        {
            switch (activePlayer)
            {
                case ActivePlayer.Explosion:
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                    Die();
                    break;
                case ActivePlayer.Gravity:
                    Physics2D.gravity *= -1;
                    animator.SetTrigger("AbilityGravity");
                    break;
                case ActivePlayer.Slime:
                    if (_isGrounded)
                        animator.SetTrigger("AbilitySlime");
                    break;
                case ActivePlayer.Teleport:
                    animator.SetTrigger("AbilityTeleport");
                    break;
                default:
                    break;
            }
        }
    }

    public void SpawnSlime()
    {
        Instantiate(slime, slimePlaceholder.position, transform.rotation);
    }

    public void RestrictMovement()
    {
        _canMove = false;
        print(_canMove);
    }

    public void AllowMovement()
    {
        _canMove = true;
        print(_canMove);
    }

    private void Die()
    {
        myProperties.isDead = true;
        _gm.KillPlayer(myProperties);
    }
}
