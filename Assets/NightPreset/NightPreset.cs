using UnityEngine;

[CreateAssetMenu(fileName = "NightPreset", menuName = "Riffat/NightPreset")]
public class NightPreset : ScriptableObject
{
    public int Night;

    public bool EnableHarris;
    public bool EnableMichael;
    public bool EnableJessie;
    public bool EnableCyntia;

    public int HarrisDifficulty;
    public int MichaelDifficulty;
    public int JessieDifficulty;
    public int CyntiaDifficulty;

    
}
