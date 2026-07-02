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
    int VSync;
    int MaxFPS;
    int GraphicsQuality;
    int FirstLaunched;
    [SerializeField] SelectionOption ResolutionOpt;
    [SerializeField] SelectionOption VSyncOpt;
    [SerializeField] SelectionOption FPSCapOpt;
    [SerializeField] SelectionOption GraphicsQualityOpt;
    [SerializeField] TuningSection[] Tunings;
    [SerializeField] AudioTuning ATuning;
    //Audio
    [SerializeField] Slider BGMVol;
    [SerializeField] Slider SFXVol;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DSL = DataSaveLoader.instance;

        FirstLaunched = DSL.GetData<int>("FirstLaunched");
        Debug.LogError($"FirstLaunched {FirstLaunched}");
        if (FirstLaunched == 1)
        {
            LastNight = DSL.GetData<int>("LastNight");

            Language = DSL.GetData<int>("Language");
            
            if (Language == 0)
            {
                LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.English;
            }
            else if (Language == 1)
            {
                LocalizationManager.Instance.currentLanguage = LocalizationManager.Language.Indonesian;
            }

            Subtitles = DSL.GetData<bool>("Subtitles");
            SubtitlesOpt.ChangeData(Subtitles? 1:0);

            ResolutionValue = DSL.GetData<int>("Resolution");
            GraphicsQuality = DSL.GetData<int>("GFXQuality");
            VSync = DSL.GetData<int>("VSync");
            MaxFPS = DSL.GetData<int>("MaxFPS");
            BGMVol.value = DSL.GetData<float>("BGM");
            SFXVol.value = DSL.GetData<float>("SFX");



            LanguageOpt.ChangeData(Language);

            ResolutionOpt.ChangeData(ResolutionValue,true);

            
            VSyncOpt.ChangeData(VSync);


            FPSCapOpt.ChangeData(MaxFPS);

            
            
            GraphicsQualityOpt.ChangeData(GraphicsQuality);




            GameSystem.instance.SelectedNight = LastNight;


            Debug.LogError($"MaxFPS {MaxFPS}");

        }
        else
        {
            DSL.SetData("FirstLaunched", 1);
            Debug.LogError($"FirstLaunched set into 1");
            foreach (TuningSection TS in Tunings)
            {
                TS.Apply();
            }
            ATuning.ApplyDefault();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
