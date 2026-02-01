using DG.Tweening;
using UnityEngine;

public class CCTVManager : MonoBehaviour
{
    public static CCTVManager instance { get; private set; }
    [SerializeField] MeshRenderer m_MeshRenderer;
    [SerializeField] GameObject Canvas;
    [SerializeField] CamButton[] buttons;
    [SerializeField] int currentCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Initialize()
    {

    }
    public void OnUse()
    {
        Canvas.SetActive(true);
        DOVirtual.Float(0,0.12f,.5f,v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic);
        Initialize();

    }
    public void OnUnUse()
    {
        DOVirtual.Float(0.12f,0, .5f, v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            Canvas.SetActive(false);
        });

    }

    public void ChangeCam(int target)
    {
        buttons[currentCam].Camera.SetActive(false);
        buttons[currentCam].Fill.SetActive(false);
        currentCam = target;
        DOVirtual.Float(0, 0.12f, .5f, v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic);

        buttons[currentCam].Camera.SetActive(true);
        buttons[currentCam].Fill.SetActive(true);
    }
}
