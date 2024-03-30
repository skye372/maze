using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    public UnityEvent PlayerDetected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        PlayerDetected?.Invoke();
    }
}
