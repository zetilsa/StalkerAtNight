using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class DepthOfFieldDynamic : MonoBehaviour
{
    [SerializeField] RaycastManager RM;
    [SerializeField] float currentValue;
    [SerializeField] bool Toggledynamic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Toggledynamic == true)
        {
            currentValue = Mathf.Lerp(currentValue, RM.currentRaycastDistance, Time.deltaTime * 2);
        }
        PostProcessingModifier.instance.DofFocusDistance = currentValue;
        
    }
}
