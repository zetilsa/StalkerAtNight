using DG.Tweening;
using KinoGlitch;
using Unity.Cinemachine;
using UnityEngine;

public class GlitchCCTV : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float detectionRadius = 50f;
    public bool isEnemyVisible;
    bool camerastate;
    private Camera cam;
    [SerializeField] AnalogGlitchController AGC;
    [SerializeField] float[] NormalGlitchValue;
    [SerializeField] float[] DetectedGlitchValue;
    float x;
    private Tween CurrentTween;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        if (isEnemyVisible == true && camerastate == false)
        {
            camerastate = true;
            CurrentTween.Kill();
            CurrentTween = DOVirtual.Float(x, 1, 1, v =>
            {
                x = v;
                AGC.ScanLineJitter = Mathf.Lerp(NormalGlitchValue[0], DetectedGlitchValue[0],v);
                AGC.VerticalJump = Mathf.Lerp(NormalGlitchValue[1], DetectedGlitchValue[1], v);
                AGC.HorizontalShake = Mathf.Lerp(NormalGlitchValue[2], DetectedGlitchValue[2], v);
                AGC.ColorDrift = Mathf.Lerp(NormalGlitchValue[3], DetectedGlitchValue[3], v);
                AGC.HorizontalRipple = Mathf.Lerp(NormalGlitchValue[4], DetectedGlitchValue[4], v);
            });
        }
        else if (isEnemyVisible == false && camerastate == true)
        {
            camerastate = false;
            CurrentTween.Kill();
            CurrentTween = DOVirtual.Float(x, 0, 1, v =>
            {
                x = v;
                AGC.ScanLineJitter = Mathf.Lerp(NormalGlitchValue[0], DetectedGlitchValue[0], v);
                AGC.VerticalJump = Mathf.Lerp(NormalGlitchValue[1], DetectedGlitchValue[1], v);
                AGC.HorizontalShake = Mathf.Lerp(NormalGlitchValue[2], DetectedGlitchValue[2], v);
                AGC.ColorDrift = Mathf.Lerp(NormalGlitchValue[3], DetectedGlitchValue[3], v);
                AGC.HorizontalRipple = Mathf.Lerp(NormalGlitchValue[4], DetectedGlitchValue[4], v);
            });

        }
    }
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        bool foundOne = false;

        foreach (Collider col in enemiesAround)
        {
            if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                foundOne = true;
                break;
            }
        }

        isEnemyVisible = foundOne;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
