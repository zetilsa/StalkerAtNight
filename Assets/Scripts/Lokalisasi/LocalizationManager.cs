using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

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
            Destroy(gameObject);
        }
    }

    public string GetText(string key)
    {
        return localizationData.GetText(key, currentLanguage);
    }
}
