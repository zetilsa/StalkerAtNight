using DG.Tweening;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance {  get; private set; }
    public bool AutoFadeOnStart;
    [SerializeField] CanvasGroup cg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        if(AutoFadeOnStart == true)
        {
            
            cg.DOFade(0, 1);
        }
    }

    public void Fade(bool state)
    {
        if (state == false)
        {
            cg.DOFade(0, 1);
        }
        else if(state == true)
        {
            cg.DOFade(1, 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
