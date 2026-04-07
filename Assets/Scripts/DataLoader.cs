using UnityEngine;

public class MenuDataLoader : MonoBehaviour
{
    DataSaveLoader DSL;
    int LastNight;

    int MusicVolume;
    int SoundVolume;

    int GraphicsQuality;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  

        LastNight = DSL.GetData<int>("LastNight");

        MusicVolume = DSL.GetData<int>("MusicVolume");
        SoundVolume = DSL.GetData<int>("SoundVolume");

        GraphicsQuality = DSL.GetData<int>("GraphicsQuality");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
