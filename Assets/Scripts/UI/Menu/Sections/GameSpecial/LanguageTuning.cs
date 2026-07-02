using UnityEngine;

public class LanguageTuning : MonoBehaviour
{
    [SerializeField] SelectionOption LanguageOption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set()
    {
        switch (LanguageOption.value)
        {
            case 0:
                LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.English;
                
            break;
            case 1:
                LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.Indonesian;
            break;
        }
        LocalizationManager.Instance.Refresh();
        
    }
}
