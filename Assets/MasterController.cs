using UnityEngine;

public class MasterController : MonoBehaviour
{
    public static MasterController instance { get; private set; }
    bool ControlEnabled;
    public bool setOnStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        if(setOnStart == true)
        {
            OverrideControl();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState()
    {
        ControlEnabled = !ControlEnabled;
        OverrideControl();
    }

    public void SetState(bool value)
    {
        ControlEnabled = value;
        OverrideControl();
    }

    void OverrideControl()
    {
        if (ControlEnabled == true)
        {
            GameManager.instance.MainInput.Player.Enable();
            GameManager.instance.MainInput.UI.Enable();
            GameManager.instance.MainInput.Computer.Enable();
            RaycastManager.Instance.EnableRaycast = true;

        }
        else if(ControlEnabled == false)
        {
            GameManager.instance.MainInput.Player.Disable();
            GameManager.instance.MainInput.UI.Disable();
            GameManager.instance.MainInput.Computer.Disable();
            RaycastManager.Instance.EnableRaycast = false;

        }
    }
}
