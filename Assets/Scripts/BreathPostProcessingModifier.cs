using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class BreathPostProcessingModifier : MonoBehaviour
{
    [SerializeField] Vector2 VignetteValueRange;
    [SerializeField] Vector2 ExposureValueRange;
    [SerializeField] Vector2 ChromaticValueRange;
    
    private Vignette Vignette;
    private ChromaticAberration Chromatic;
    private ColorAdjustments colorAdjustments;
    float value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        value = PlayerManager.instance.breath;
        PostProcessingModifier.instance.VignetteIntensity = Mathf.Lerp(VignetteValueRange.x, VignetteValueRange.y, value / 100);
        PostProcessingModifier.instance.PostExposure = Mathf.Lerp(ExposureValueRange.x, ExposureValueRange.y, value / 100);
        PostProcessingModifier.instance.ChromaticIntensity = Mathf.Lerp(ChromaticValueRange.x, ChromaticValueRange.y, value / 100);
        
    }
}
