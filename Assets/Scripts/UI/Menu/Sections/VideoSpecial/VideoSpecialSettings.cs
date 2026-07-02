using DG.Tweening;
using UnityEngine;

public class VideoSpecialSettings : MonoBehaviour
{
    [SerializeField] SelectionOption VSyncOption;
    [SerializeField] SelectionOption ResolutionOption;
    [SerializeField] ResolutionFinder ResFinder;
    [SerializeField] RectTransform GFXOption;
    [SerializeField] CanvasGroup MaxFPSOption;
    [SerializeField] SelectionOption MaxFPSOpt;

    int MaxFrameRate;
    bool VsyncEnabled;

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

    public void ChangeResolution()
    {
        Resolution getRes = ResFinder.AvailableRes[ResolutionOption.value];
        Screen.SetResolution(getRes.width, getRes.height, FullScreenMode.ExclusiveFullScreen, getRes.refreshRateRatio);
    }
    public void VSyncChanged()
    {
        if (state != VSyncOption.value)
        {
            state = VSyncOption.value;
            if (VSyncOption.value == 0)
            {
                VsyncEnabled = true;
                MaxFPSOption.DOFade(0, 1).SetEase(Ease.OutCubic);
                GFXOption.DOLocalMoveY(-140, 1).SetEase(Ease.OutCubic);
            }
            else if (VSyncOption.value == 1)
            {
                VsyncEnabled = false;
                MaxFPSOption.DOFade(1, 1).SetEase(Ease.OutCubic);
                GFXOption.DOLocalMoveY(-190, 1).SetEase(Ease.OutCubic);
            }
            QualitySettings.vSyncCount = VsyncEnabled ? 1 : 0;

        }
        if (VsyncEnabled == false)
        {
            Application.targetFrameRate = MaxFrameRate;
        }
    }

    public void MaxFPSChanged()
    {
        if (MaxFPSOpt.value == 3)
        {
            MaxFrameRate = -1;

            return;
        }
        
        if (int.TryParse(MaxFPSOpt.texts[MaxFPSOpt.value], out int maxfps))
        {
            MaxFrameRate = maxfps;
        }
    }
}
