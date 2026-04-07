using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InGamePauseSystem : MonoBehaviour
{
    public static InGamePauseSystem instance {  get; private set; }
    public bool EnablePausing;
    [SerializeField] CanvasGroup cg;
    bool currentpausestate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Application.focusChanged += v =>
        {
            if(v == false)
            {
                Pause(true);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (EnablePausing == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause(true);
            }
        }
    }

    public void Pause(bool state)
    {
        if (currentpausestate != state)
        {
            currentpausestate = state;
            if (currentpausestate == true)
            {
                PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, false, false, false);
                DOVirtual.Float(1, 0, .5f, v =>
                {
                    Time.timeScale = v;
                }).SetUpdate(true);
                cg.DOFade(1, .5f).OnComplete(() =>
                {
                    cg.blocksRaycasts = true;
                    PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, true, false, false);
                }).SetUpdate(true);
            }
            else if (currentpausestate == false)
            {
                cg.blocksRaycasts = false;
                DOVirtual.Float(0, 1, .5f, v =>
                {
                    Time.timeScale = v;
                }).SetUpdate(true);
                cg.DOFade(0, .5f).OnComplete(() =>
                {
                    PlayerManager.instance.ChangeControlState(true, true, true, true, true, false, false, true, false);
                }).SetUpdate(true);
            }
        }
    }

    public void SetEnableState(bool state)
    {
        EnablePausing = state;
    }
}
