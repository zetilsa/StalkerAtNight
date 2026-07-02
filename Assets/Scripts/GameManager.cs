using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public AudioSource HeartBeatSFX;
    public AudioSource BreathingSFX;
    public FirstPersonController MainFPS;
    public Transform CameraJoint;
    public bool started;
    public InputSystem_Actions MainInput;
    PlayableDirector PlayableDirector;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] AudioMixer mixer;
    [SerializeField] PlayableDirector StartBedAnimator;
    [SerializeField] GameObject PostDeathUI;
    [SerializeField] GameObject WinUI;
    public CinemachineBrain Camera;
    public Transform DefaultSpawnPoint;
    void OnEnable()
    {
        MainInput = new InputSystem_Actions();

        if (instance == null)
        {
            instance = this;
        }

    }
    public void SetCameraBlendValue(float Value)
    {
        Camera.DefaultBlend.Time = Value;
    }
    private void OnDisable()
    {

    }
    private void Awake()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.Reset();
        }
    }
    private void Start()
    {
        StartBedAnimator.Play();
    }
    public void StartGame()
    {
        
        if (started == false)
        {
            started = true;
            
            MainFPS.playerCanMove = true;
            MainFPS.cameraCanMove = true;
            MainFPS.enableSprint = true;

            IEnumerator GuideMove()
            {
                GuideTextManager.instance.SetText(new string[1] { "[WASD] Text_Guide06" });
                GuideTextManager.instance.Show();
                yield return new WaitForSeconds(4);
                GuideTextManager.instance.Hide();
            }
            StartCoroutine(GuideMove());
        }
    }

    public void Win()
    {
        InGamePauseSystem.instance.EnablePausing = false;
        WinUI.SetActive(true);
        //DOVirtual.Float(0, -80, 1, v =>
        //{
        //    GameVolume = v;
        //}).SetEase(Ease.InCubic);
        
        AIManager.instance.StopTick();
    }
    public void GoThankyou()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void DoneTransition()
    {
        PlayerManager.instance.Transition = false;
    }

    public void RegisterTimelinePlayback(PlayableDirector obj)
    {
        PlayableDirector = obj;
        PlayableDirector.played += TimelinePlaying;
        PlayableDirector.stopped += TimelineStopped;
        PlayableDirector.Play();
    }

    void TimelinePlaying(PlayableDirector pd)
    {
        print("timelinestarted");
        PlayerManager.instance.Transition = true;
    }
    void TimelineStopped(PlayableDirector pd)
    {

        print("timelinestopped");
        PlayerManager.instance.Transition = false;
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void PostJumpscare()
    {
        MainFPS.gameObject.SetActive(false);
        if (GameSystem.instance != null)
        {
            GameSystem.instance.SetVolume(0, 1);
        }
        else
        {
            GameManager.instance.mixer.SetFloat("GameVolume", -80);
        }
            PostDeathUI.SetActive(true);

    }
    public void GoRetry()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void FadeVolume()
    {
        if(GameSystem.instance != null)
        {
            GameSystem.instance.SetVolume(1, 3);
            print("pakeGamesystem");
        }
        else
        {
            print("gakpakeGamesystem");
            DOVirtual.Float(-80, 0, 1, v =>
            {
                mixer.SetFloat("GameVolume", 0);

            }).SetEase(Ease.OutCubic);

        }
    }
}
