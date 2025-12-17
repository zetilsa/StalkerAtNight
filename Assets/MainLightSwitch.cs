using UnityEngine;

public class MainLightSwitch : MonoBehaviour
{
    bool state;
    [SerializeField] LightManager[] lights;

    [SerializeField] ReflectionProbe reflection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        state = !state;
        foreach (LightManager light in lights)
        {
            light.ChangeStateLight(state);
        }
        reflection.enabled = state;
        

    }
}
