using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class ResolutionFinder : MonoBehaviour
{
    [SerializeField] SelectionOption ResolutionOption;
    [SerializeField] TextMeshProUGUI RefreshRateText;
    public List<Resolution> AvailableRes = new List<Resolution>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for(int x = 0; x < Screen.resolutions.Length; x++)
        {
            if ((Mathf.Round((float)Screen.resolutions[x].refreshRateRatio.value)) - (Mathf.Round((float)Screen.currentResolution.refreshRateRatio.value)) == 0)
            {
                Debug.Log($"added {Screen.resolutions[x].width}x{Screen.resolutions[x].height}@{Screen.resolutions[x].refreshRateRatio.value}");
                AvailableRes.Add(Screen.resolutions[x]);
            }
        }
        ResolutionOption.texts = new string[AvailableRes.Count];
        for (int x = 0; x < AvailableRes.Count; x++)
        {
            ResolutionOption.texts[x] = $"{AvailableRes[x].width}x{AvailableRes[x].height}";
            if (Screen.currentResolution.width == AvailableRes[x].width && Screen.currentResolution.height == AvailableRes[x].height)
            {
                ResolutionOption.ChangeData(x,true);
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
