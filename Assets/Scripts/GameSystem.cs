using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameSystem : MonoBehaviour
{
    public static GameSystem instance { get; private set; }
    public DataSaveLoader DSL;
    [SerializeField] NightPreset[] Nights;
    public NightPreset Night;
    public int SelectedNight;
    public AudioMixer Mixer;

    [SerializeField] int GameSceneBuildIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (GameSystem.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveNight()
    {
        DSL.SetData("LastNight", SelectedNight);
    }
    public void SaveNightAndContinue()
    {
        SelectedNight += 1;
        SaveNight();
        SelectNight(SelectedNight);
        SceneManager.LoadScene(GameSceneBuildIndex);

    }
    public void NewGame()
    {
        SelectedNight = 0;
        SaveNight();
        StartGame();
    }
    public void Continue()
    {

    }
    public void SelectNight(int i)
    {
        Night = Nights[i];
    }

    public void SetVolume(float linearValue)
    {
        // Clamp nilai agar berada di rentang 0.0001 - 1
        float volume = Mathf.Clamp(linearValue, 0.0001f, 1f);

        // Konversi ke Decibel
        float dB = Mathf.Log10(volume) * 20;

        Mixer.SetFloat("GameVolume", dB);
    }
    public void SetVolume(string parameterName ,float linearValue)
    {
        // Clamp nilai agar berada di rentang 0.0001 - 1
        float volume = Mathf.Clamp(linearValue, 0.0001f, 1f);

        // Konversi ke Decibel
        float dB = Mathf.Log10(volume) * 20;

        Mixer.SetFloat(parameterName, dB);
    }
    public void SetVolume(float linearValue,float time)
    {
        // Clamp nilai agar berada di rentang 0.0001 - 1
        float volume = Mathf.Clamp(linearValue, 0.0001f, 1f);

        // Konversi ke Decibel
        float dB = Mathf.Log10(volume) * 20;

        Mixer.DOSetFloat("GameVolume", dB, time);
    }

    public void StartGame()
    {
        IEnumerator w()
        {
            DOVirtual.Float(1, 0, 1, v =>
            {
                SetVolume(v);
            });
            FadeManager.instance.Fade(true);
            yield return new WaitForSeconds(1);
            LoadingScreen.instance.GetComponent<CanvasGroup>().DOFade(1, 1);
            yield return new WaitForSeconds(7);
            LoadingScreen.instance.GetComponent<CanvasGroup>().DOFade(0, 1).OnComplete(() =>
            {
                SceneManager.LoadScene(GameSceneBuildIndex);
            });
            
        }
        StartCoroutine(w());
    }

    public void Leave()
    {
        Application.Quit();
    }
}
