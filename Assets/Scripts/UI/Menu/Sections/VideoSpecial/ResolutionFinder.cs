using UnityEngine;


public class ResolutionFinder : MonoBehaviour
{
    [SerializeField] SelectionOption ResolutionOption;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ResolutionOption.texts = new string[Screen.resolutions.Length];
        for (int x = 0; x < Screen.resolutions.Length; x++)
        {
            ResolutionOption.texts[x] = $"{Screen.resolutions[x].width}x{Screen.resolutions[x].height}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
