using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessingSettings : MonoBehaviour
{
    [SerializeField] VolumeProfile[] profiles;
    [SerializeField]Volume volume;
    int getData = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        getData = PlayerPrefs.GetInt("GraphicsQuality");
        volume.profile = profiles[getData];
        print(getData);
    }
}
