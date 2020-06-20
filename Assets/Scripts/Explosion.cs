using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            other.GetComponent<IInteractable>().Interact();
        }
    }
}
