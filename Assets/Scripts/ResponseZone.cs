using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ResponseZone : MonoBehaviour
{
    public event System.Action OnPlayerEntered;
    public event System.Action OnPlayerLeft;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
            OnPlayerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
            OnPlayerLeft?.Invoke();
    }
}
