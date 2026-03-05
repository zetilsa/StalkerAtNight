using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class DepthOfFieldDynamic : MonoBehaviour
{
    [SerializeField] RaycastManager RM;
    [SerializeField] Volume globalVolume;
    private DepthOfField dof;
    [SerializeField] float currentValue;
    [SerializeField] bool Toggledynamic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get DOF from the volume profile
        if (globalVolume.profile.TryGet(out dof))
        {
            // Enable override so runtime changes take effect
            dof.focalLength.overrideState = true;
        }
        else
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Toggledynamic == true)
        {
            currentValue = Mathf.Lerp(currentValue, RM.currentRaycastDistance, Time.deltaTime * 2);
        }
            dof.focusDistance.value = currentValue;
        
    }
}
