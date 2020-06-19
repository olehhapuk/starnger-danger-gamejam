using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager _gm;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * 15 * Time.deltaTime, 0));

        if (Input.GetButtonDown("Fire2"))
        {
            Die();
        }
    }

    private void Die()
    {
        _gm.ChangePlayer();
        Destroy(gameObject);
    }
}
