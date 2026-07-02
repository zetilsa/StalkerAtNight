using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    string key;
    void Start()
    {
        if (textUI == null)
            textUI = GetComponent<TextMeshProUGUI>();
        key = textUI.text;
        LocalizationManager.Instance.Texts.Add(this);
        UpdateText();
    }
    public void SetKey(string keyvalue)
    {
        key = keyvalue;
    }
    public void UpdateText()
    {
        string original = key;
        string result = original;

        foreach (var entry in LocalizationManager.Instance.localizationData.strings)
        {
            // Cek apakah key ada di teks
            if (result.Contains(entry.key))
            {
                string localized = LocalizationManager.Instance.currentLanguage ==
                    LocalizationManager.Language.Indonesian ? entry.indonesian : entry.english;

                // Ganti semua occurrence key dengan versi lokal
                result = result.Replace(entry.key, localized);
            }
        }

        textUI.text = result;
    }

    public void UpdateTextStatic()
    {
        string original = textUI.text;
        string result = original;

        foreach (var entry in LocalizationManager.Instance.localizationData.strings)
        {
            // Cek apakah key ada di teks
            if (result.Contains(entry.key))
            {
                print($"{result} contained {entry.key}");
                string localized = LocalizationManager.Instance.currentLanguage ==
                    LocalizationManager.Language.Indonesian ? entry.indonesian : entry.english;

                // Ganti semua occurrence key dengan versi lokal
                result = result.Replace(entry.key, localized);
            }
        }

        textUI.text = result;
    }
}
