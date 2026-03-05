using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
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

    }
    private void OnLevelWasLoaded(int level)
    {
        Mixer.SetFloat("GameVolume", -80);
        if (AutoFadeOnStart == true)
        {
            IEnumerator i()
            {
                yield return new WaitForSeconds(2);
                cg.DOFade(0, 2).OnComplete(() =>
                {
                    Mixer.DOSetFloat("GameVolume", 0, 2).SetEase(Ease.OutCubic);
                });
                
            }
            
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
