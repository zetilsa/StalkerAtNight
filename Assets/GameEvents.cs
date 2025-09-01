// GameEvents.cs (or a similar central script)
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    // Make it a singleton for easy access from other scripts
    public static GameEvents Instance { get; private set; }

    private void Start()
    {

            Instance = this;
        

    }

    // Declare the delegate (the signature of the method that will be called)
    public delegate void FirstTransitionComplete();
    // Declare the event using the delegate
    public event FirstTransitionComplete OnFirstTransitionComplete;

    // A method to trigger the event
    public void TriggerFirstTransitionComplete()
    {
        print("tes2");
        // Null-check to prevent errors if no one is listening
        if (OnFirstTransitionComplete != null)
        {
            OnFirstTransitionComplete();
        }
    }
}