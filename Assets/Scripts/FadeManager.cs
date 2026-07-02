using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    public static FadeManager instance {  get; private set; }
    public bool AutoFadeOnStart;
    [SerializeField] CanvasGroup cg;
    [SerializeField] AudioMixer Mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        SLoaded();
    }
    private void SLoaded()
    {
        cg.blocksRaycasts = true;
        GameSystem.instance.SetVolume(0);
        
        if (AutoFadeOnStart == true)
        {
            IEnumerator i()
            {
                yield return new WaitForSeconds(2);
                GameSystem.instance.SetVolume(1, 2);
                cg.DOFade(0, 2).OnComplete(() =>
                {
                    cg.blocksRaycasts = false;
                });
                
            }
            StartCoroutine(i());
            
        }
    }

    public void FadeStart()
    {
        GameSystem.instance.SetVolume(0);
            IEnumerator i()
            {
                yield return new WaitForSeconds(2);
                cg.DOFade(0, 2).OnComplete(() =>
                {
                    GameSystem.instance.SetVolume(1, 1);
                });

            }
            StartCoroutine(i());
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
