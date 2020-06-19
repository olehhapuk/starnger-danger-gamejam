using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isActive;

    private void Update()
    {
        if (!isActive)
            return;
        
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * 15 * Time.deltaTime, 0));
    }
}
