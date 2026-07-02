using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    public List<LocalizedText> Texts = new List<LocalizedText>();
    public enum Language
    {
        Indonesian,
        English
    }

    public Language currentLanguage = Language.Indonesian;
    public LocalizationData localizationData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(LocalizationManager.Instance);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Reset()
    {
        Texts = new List<LocalizedText>();
    }
    public void Refresh()
    {
        print("refreshing...");
        foreach(LocalizedText text in Texts)
        {
            text.UpdateText();
            print($"refreshes {text.gameObject.name}");
        }
    }
    public string GetText(string key)
    {
        return localizationData.GetText(key, currentLanguage);
    }
}
