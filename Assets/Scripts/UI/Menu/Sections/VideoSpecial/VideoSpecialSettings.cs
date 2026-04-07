using DG.Tweening;
using UnityEngine;

public class VideoSpecialSettings : MonoBehaviour
{
    [SerializeField] SelectionOption VSyncOption;
    [SerializeField] RectTransform GFXOption;
    [SerializeField] CanvasGroup MaxFPSOption;
    int state = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        VSyncChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VSyncChanged()
    {
        if (state != VSyncOption.value)
        {
            state = VSyncOption.value;
            if (VSyncOption.value == 0)
            {
                MaxFPSOption.DOFade(0, 1).SetEase(Ease.OutCubic);
                GFXOption.DOLocalMoveY(-140, 1).SetEase(Ease.OutCubic);
            }
            else if (VSyncOption.value == 1)
            {
                MaxFPSOption.DOFade(1, 1).SetEase(Ease.OutCubic);
                GFXOption.DOLocalMoveY(-190, 1).SetEase(Ease.OutCubic);
            }
        }
    }
}
