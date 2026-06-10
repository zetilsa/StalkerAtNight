using UnityEngine;
using UnityEngine.UI;

public class MenuDataLoader : MonoBehaviour
{
    DataSaveLoader DSL;
    public int LastNight;

    //Game
    int Language;
    bool Subtitles;
    [SerializeField] SelectionOption LanguageOpt;
    [SerializeField] SelectionOption SubtitlesOpt;

    //Video
    int ResolutionValue;
    bool VSync;
    int MaxFPS;
    int GraphicsQuality;
    [SerializeField] SelectionOption ResolutionOpt;
    [SerializeField] SelectionOption VSyncOpt;
    [SerializeField] SelectionOption FPSCapOpt;
    [SerializeField] SelectionOption GraphicsQualityOpt;

    //Audio
    [SerializeField] Slider BGMVol;
    [SerializeField] Slider SFXVol;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        DSL = DataSaveLoader.instance;
        LastNight = DSL.GetData<int>("LastNight");

        Language = DSL.GetData<int>("Language");
        if (Language == 0) {
            LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.English;
        }
        else if (Language == 1)
        {
            LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.Indonesian;
        }
        Subtitles = DSL.GetData<bool>("Subtitles");
        
        
        ResolutionValue = DSL.GetData<int>("Resolution");
        VSync = DSL.GetData<bool>("VSync");
        MaxFPS = DSL.GetData<int>("MaxFPS");
        GraphicsQuality = DSL.GetData<int>("GFXQuality");

        
        BGMVol.value = DSL.GetData<int>("BGM");
        SFXVol.value = DSL.GetData<int>("SFX");



        GameSystem.instance.SelectedNight = LastNight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
