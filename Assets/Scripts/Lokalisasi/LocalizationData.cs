using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [System.Serializable]
    public class LocalizedString
    {
        public string key;      // KEY unik, contoh: MENU_PLAY
        public string indonesian;
        public string english;
    }

    public List<LocalizedString> strings = new List<LocalizedString>();

    public string GetText(string key, LocalizationManager.Language lang)
    {
        var entry = strings.Find(s => s.key == key);
        if (entry == null)
        {
            Debug.LogWarning($"Key '{key}' tidak ditemukan!");
            return $"[{key}]";
        }

        return lang == LocalizationManager.Language.Indonesian
            ? entry.indonesian
            : entry.english;
    }
}
