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
        GameManager.instance.MainFPS.enabled = ControlEnabled;
        RaycastManager.Instance.enabled = ControlEnabled;
    }
}
