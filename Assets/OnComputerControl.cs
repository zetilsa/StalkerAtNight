using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class OnComputerControl : MonoBehaviour
{
    

    public InputActionAsset inputActionsAsset;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
        inputActionsAsset.actionMaps[2].actions[0].performed += ExitCam;
        
    }

    private void OnDisable()
    {
        inputActionsAsset.actionMaps[2].actions[0].performed -= ExitCam;
    }

    private void ExitCam(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Console.Beep();
        RaycastManager.Instance.ExitCamera();
    }
}
