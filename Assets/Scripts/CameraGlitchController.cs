using KinoGlitch;
using UnityEngine;

public class CameraGlitchController : MonoBehaviour
{
    AnalogGlitchController AGC;
    DigitalGlitchController DGC;

    public float ScanlineJitter;
    public float VerticalJump;
    public float HorizontalShake;
    public float ColorDrift;
    public float HorizontalRipple;
    public float DigitalGlitchValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AGC = GameManager.instance.Camera.GetComponent<AnalogGlitchController>();
        DGC = GameManager.instance.Camera.GetComponent<DigitalGlitchController>();
    }

    // Update is called once per frame
    void Update()
    {
        AGC.ScanLineJitter = ScanlineJitter;
        AGC.VerticalJump = VerticalJump;
        AGC.HorizontalShake = HorizontalShake;
        AGC.ColorDrift = ColorDrift;
        AGC.HorizontalRipple = HorizontalRipple;
        DGC.Intensity = DigitalGlitchValue;
    }
}
