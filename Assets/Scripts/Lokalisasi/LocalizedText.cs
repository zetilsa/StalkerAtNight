using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    void Awake()
    {
        if (textUI == null)
            textUI = GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    public void UpdateText()
    {
        string original = textUI.text;
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
}
