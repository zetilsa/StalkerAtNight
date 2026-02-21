using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class BreathPostProcessingModifier : MonoBehaviour
{
    [SerializeField] Vector2 VignetteValueRange;
    [SerializeField] Vector2 ExposureValueRange;
    [SerializeField] Vector2 ChromaticValueRange;
    [SerializeField] Volume globalVolume;
    private Vignette Vignette;
    private ChromaticAberration Chromatic;
    private ColorAdjustments colorAdjustments;
    float value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get DOF from the volume profile
        if (globalVolume.profile.TryGet(out Vignette))
        {
            // Enable override so runtime changes take effect
            Vignette.intensity.overrideState = true;
        }
        else
        {
            this.enabled = false;
        }

        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            // Enable override so runtime changes take effect
            colorAdjustments.postExposure.overrideState = true;
        }
        else
        {
            this.enabled = false;
        }

        if (globalVolume.profile.TryGet(out Chromatic))
        {
            // Enable override so runtime changes take effect
            Chromatic.intensity.overrideState = true;
        }
        else
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

            value = PlayerManager.instance.breath;
            Vignette.intensity.value = Mathf.Lerp(VignetteValueRange.x, VignetteValueRange.y, value / 100);
            colorAdjustments.postExposure.value = Mathf.Lerp(ExposureValueRange.x, ExposureValueRange.y, value / 100);
        Chromatic.intensity.value = Mathf.Lerp(ChromaticValueRange.x, ChromaticValueRange.y, value / 100);
        
    }
}
