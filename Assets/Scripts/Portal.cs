using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public bool isBeingUsed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (otherPortal == null)
            return;
        
        if (other.CompareTag("Player"))
        {
            if (isBeingUsed)
                isBeingUsed = false;
            else
            {
                otherPortal.isBeingUsed = true;
                other.transform.position = otherPortal.transform.position;
            }
        }
    }
}
