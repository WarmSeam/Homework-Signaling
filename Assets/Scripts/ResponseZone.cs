using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ResponseZone : MonoBehaviour
{
    private BoxCollider _collider;

    public event System.Action PlayerEntered;
    public event System.Action PlayerExited;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out _))
            PlayerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
            PlayerExited?.Invoke();
    }
}
