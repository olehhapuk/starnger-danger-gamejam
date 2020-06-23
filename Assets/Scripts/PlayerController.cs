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

    [SerializeField] private Transform spawner;
    [SerializeField] private PlayerAudio playerAudio;
    
    [Header("Check if grounded")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("Gravity")] [SerializeField] private int maxGravitySwitches = 3;

    [Header("Slime")]
    [SerializeField] private GameObject slime;
    [SerializeField] private Transform slimePlaceholder;

    [Header("Portal")]
    [SerializeField] private GameObject portal;

    private List<Portal> _portals;
    
    private GameManager _gm;
    private Rigidbody2D _rb;
    private PlayerAudio _playerAudio;

    private float _moveDir;
    private bool _canMove = true;
    private bool _isGrounded;
    private int _gravitySwitchesLeft;
    private int _gravityModifier = 1;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _playerAudio = GetComponent<PlayerAudio>();
        animator = GetComponent<Animator>();
        _portals = new List<Portal>(2);
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, -Mathf.Abs(Physics2D.gravity.y));
    }

    private void Start()
    {
        _canMove = true;
        _gravitySwitchesLeft = maxGravitySwitches;
        _gravityModifier = 1;
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
        if (_gravityModifier == -1)
        {
            if (_moveDir < 0)
                transform.eulerAngles = new Vector2(180, 180);
            else if (_moveDir > 0)
                transform.eulerAngles = new Vector2(180, 0);
        }
        else if (_gravityModifier == 1)
        {
            if (_moveDir < 0)
                transform.eulerAngles = new Vector2(0, 180);
            else if (_moveDir > 0)
                transform.eulerAngles = new Vector2(0, 0);
        }
    }

    private void GetInput()
    {
        _moveDir = Input.GetAxisRaw("Horizontal");
        
        if (_isGrounded)
            animator.SetFloat("Speed", Mathf.Abs(_moveDir));
        else
            animator.SetFloat("Speed", 0);


        if (Input.GetButtonDown("Jump") && _isGrounded && _gravityModifier != -1)
        {
            _rb.AddForce(new Vector2(0, myProperties.jumpForce * _gravityModifier), ForceMode2D.Impulse);
            
            switch (activePlayer)
            {
                case ActivePlayer.Explosion:
                    _playerAudio.PlayAudio("Flamey_Jump");
                    break;
                case ActivePlayer.Gravity:
                    _playerAudio.PlayAudio("Gravity_Jump");
                    break;
                case ActivePlayer.Slime:
                    _playerAudio.PlayAudio("Slime_Jump");
                    break;
                default:
                    break;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            switch (activePlayer)
            {
                case ActivePlayer.Explosion:
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                    Die();
                    break;
                case ActivePlayer.Gravity:
                    if (_gravitySwitchesLeft > 1)
                        animator.SetTrigger("AbilityGravity");
                    break;
                case ActivePlayer.Teleport:
                    if (_isGrounded)
                        SpawnPortal();
                    break;
                case ActivePlayer.Slime:
                    if (_isGrounded)
                        animator.SetTrigger("AbilitySlime");
                    break;
                default:
                    break;
            }
        }
    }

    public void FlipGravity()
    {
        _gravityModifier *= -1;
        print(_gravityModifier);
        Physics2D.gravity *= -1;
        _gravitySwitchesLeft--;

        if (_gravityModifier == -1)
        {
            transform.eulerAngles = new Vector2(180, transform.eulerAngles.y);
            playerAudio.PlayAudio("Gravity_Changed");
        }
        else
        {
            transform.eulerAngles = new Vector2(0, transform.eulerAngles.y + 180);
            playerAudio.StopAudio();
        }

        if (_gravitySwitchesLeft <= 0)
            Die();
    }

    private void SpawnPortal()
    {
        var newPortal = Instantiate(portal, transform.position, transform.rotation);
        _playerAudio.PlayAudio("Portal_Ability");
        newPortal.GetComponent<Portal>().isBeingUsed = true;
        _portals.Add(newPortal.GetComponent<Portal>());
        if (_portals.Count == 2)
        {
            _portals[0].otherPortal = _portals[1];
            _portals[1].otherPortal = _portals[0];
            _portals[0].isBeingUsed = false;
            _portals[1].isBeingUsed = false;
            _portals.Clear();
            Die();
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

    public void Die()
    {
        myProperties.isDead = true;
        _gm.KillPlayer(myProperties);
        transform.position = spawner.position;
    }
}
