using DG.Tweening;
using UnityEngine;

public class CCTVManager : MonoBehaviour
{
    public static CCTVManager instance { get; private set; }
    [SerializeField] MeshRenderer m_MeshRenderer;
    [SerializeField] GameObject Canvas;
    public CamButton[] buttons;
    public int currentCam { get; private set; }
    [SerializeField] GameObject Button;
    [SerializeField] AudioSource src;

    float currentnoisepower;

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
        buttons[currentCam].Camera.SetActive(true);
    }

    void CleanUp()
    {
        if (buttons[currentCam].Camera.GetComponent<Room>().AlwaysOn == false)
        {
            buttons[currentCam].Camera.SetActive(false);
        }
    }
    public void OnUse()
    {
        DOTween.Complete(this);
        DOVirtual.Float(m_MeshRenderer.material.GetFloat("_NoisePower"), 0.13f, .5f, v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic);
        Initialize();

    }
    public void OnUnUse()
    {

        DOVirtual.Float(m_MeshRenderer.material.GetFloat("_NoisePower"), 0, .5f, v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            CleanUp();
        });

    }

    public void ChangeCam(int target)
    {
        if (target != currentCam)
        {
            src.Play();
            DOVirtual.Float(0.13f, 0, .2f, v =>
            {
                m_MeshRenderer.material.SetFloat("_NoisePower", v);
            }).SetEase(Ease.InOutCubic);
                if (buttons[currentCam].Camera.GetComponent<Room>().AlwaysOn == false)
                {
                    buttons[currentCam].Camera.SetActive(false);
                }
                buttons[currentCam].Fill.SetActive(false);
                currentCam = target;
            if (buttons[target].isGlitching == false)
            {
                DOVirtual.Float(0, 0.13f, .5f, v =>
                {
                    m_MeshRenderer.material.SetFloat("_NoisePower", v);
                }).SetEase(Ease.InOutCubic);
            }
                buttons[currentCam].Camera.SetActive(true);
                buttons[currentCam].Fill.SetActive(true);

                if (target == 5 || target == 6 || target == 7)
                {
                    Button.SetActive(true);
                }
                else
                {
                    Button.SetActive(false);
                }
            
        }
        
    }
    public void ChangeCam(int target,bool refresh)
    {
        if (refresh == true)
        {
            DOVirtual.Float(0.13f, 0, .2f, v =>
            {
                m_MeshRenderer.material.SetFloat("_NoisePower", v);
            }).SetEase(Ease.InOutCubic);
            if (buttons[currentCam].Camera.GetComponent<Room>().AlwaysOn == false)
            {
                buttons[currentCam].Camera.SetActive(false);
            }
            buttons[currentCam].Fill.SetActive(false);
            currentCam = target;

            DOVirtual.Float(0, 0.13f, .5f, v =>
            {
                m_MeshRenderer.material.SetFloat("_NoisePower", v);
            }).SetEase(Ease.InOutCubic);

            buttons[currentCam].Camera.SetActive(true);
            buttons[currentCam].Fill.SetActive(true);

            if (target == 5 || target == 6 || target == 7)
            {
                Button.SetActive(true);
            }
            else
            {
                Button.SetActive(false);
            }
        }
        }

    
    public void Glitch()
    {
        DOVirtual.Float(m_MeshRenderer.material.GetFloat("_NoisePower"), 0, .2f, v =>
        {
            m_MeshRenderer.material.SetFloat("_NoisePower", v);
        }).SetEase(Ease.InOutCubic);
    }
}
