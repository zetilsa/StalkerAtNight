using DG.Tweening;
using NUnit.Framework;
using System.Security.Permissions;
using UnityEngine;

public class AudioSourceFade : MonoBehaviour
{
    [SerializeField] bool FadeIn;
    [SerializeField] float FadeDuration;
    [SerializeField] AudioSource src;
    [SerializeField] float defaultVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        if (FadeIn == true)
        {
            src.DOFade(1, FadeDuration);
        }
        else
        {
            src.volume = 1;
        }
    }

    public void Fade(float targetValue)
    {
        src.DOFade(targetValue, FadeDuration);
    }
    public void Fade()
    {
        src.DOFade(defaultVolume, FadeDuration);
    }
}
