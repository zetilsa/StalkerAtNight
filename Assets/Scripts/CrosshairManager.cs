using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CrosshairManager : MonoBehaviour
{
    public static CrosshairManager instance { get; private set; }
    public bool Show;
    public bool Detect;
    [SerializeField] CanvasGroup Child;
    [SerializeField] CanvasGroup MainCrosshair;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetShow(bool show)
    {
        Show = show;
        if (Show == false)
        {
            MainCrosshair.DOFade(0f, 0.5f);
        }
        else
        {
            MainCrosshair.DOFade(1, 0.5f);
        }
    }

    public void SetDetect(bool detect)
    {
        Detect = detect;
        if (detect == false)
        {
            Child.DOFade(0, 0.5f);
        }
        else if (detect == true)
        {
            print("ting");
            Child.DOFade(1, 0.5f);
        }
    }
}
