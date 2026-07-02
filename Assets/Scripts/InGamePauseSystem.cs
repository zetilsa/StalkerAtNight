using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InGamePauseSystem : MonoBehaviour
{
    public static InGamePauseSystem instance {  get; private set; }
    public bool EnablePausing;
    [SerializeField] bool currentCursorVisible;
    [SerializeField] CursorLockMode currentCursorLockMode;
    [SerializeField] bool currentRaycastEnabled;
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
            if(v == false && EnablePausing == true)
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
                Pause(!currentpausestate);
            }
        }
    }

    public void Pause(bool state)
    {
        EnablePausing = false;
        if (currentpausestate != state)
        {
            currentpausestate = state;
            if (currentpausestate == true)
            {
                currentCursorVisible = Cursor.visible;
                currentCursorLockMode = Cursor.lockState;
                currentRaycastEnabled = RaycastManager.Instance.EnableRaycast;

                RaycastManager.Instance.EnableRaycast = false;
                //PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, false, false, false);
                DOVirtual.Float(1, 0, .5f, v =>
                {
                    Time.timeScale = v;
                }).SetUpdate(true);
                cg.DOFade(1, .5f).OnComplete(() =>
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    cg.blocksRaycasts = true;
                    //PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, true, false, false);

                    EnablePausing = true;
                }).SetUpdate(true);
            }
            else if (currentpausestate == false)
            {
                Cursor.lockState = currentCursorLockMode;
                Cursor.visible = currentCursorVisible;
                cg.blocksRaycasts = false;
                DOVirtual.Float(0, 1, .5f, v =>
                {
                    Time.timeScale = v;
                }).SetUpdate(true);
                cg.DOFade(0, .5f).OnComplete(() =>
                {
                    RaycastManager.Instance.EnableRaycast = currentRaycastEnabled;
                    //PlayerManager.instance.ChangeControlState(true, true, true, true, true, false, false, true, false);
                    EnablePausing = true;
                }).SetUpdate(true);
            }
        }
    }

    public void SetEnableState(bool state)
    {
        EnablePausing = state;
    }
}
