using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject particles;
    
    public void Interact()
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
