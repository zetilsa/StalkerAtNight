using UnityEngine;
using UnityEngine.Rendering;

public class AudioTuning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGMVolume(float volume)
    {
        GameSystem.instance.SetVolume("Music",(float)volume);
        GameSystem.instance.DSL.SetData("BGM", volume);
    }


    public void SetSFXVolume(float volume)
    {
        GameSystem.instance.SetVolume("SFX", (float)volume);
        GameSystem.instance.DSL.SetData("SFX", volume);
    }

    public void ApplyDefault()
    {
        GameSystem.instance.DSL.SetData("SFX", 1);
        GameSystem.instance.DSL.SetData("BGM", 1);
    }
}
