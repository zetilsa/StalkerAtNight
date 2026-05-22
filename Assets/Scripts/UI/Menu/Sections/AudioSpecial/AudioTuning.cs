using UnityEngine;

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
    }


    public void SetSFXVolume(float volume)
    {
        GameSystem.instance.SetVolume("SFX", (float)volume);
    }
}
