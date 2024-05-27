// all the needed methods from Unity will be imported 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// die Unterklasse PlayerDetector erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class PlayerDetector : MonoBehaviour
{
    // creating a public variable of type UnityEvent
    public UnityEvent PlayerDetected;

    // creating a private method with the return type void
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player collides with a player...
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            // ...the method NextLevel is called
            NextLevel();
        }
    }

    // creating a private method of type void
    private void NextLevel()
    {
        // gets invoked, when the player is detected
        PlayerDetected?.Invoke();
    }
}
