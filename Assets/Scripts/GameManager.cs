using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public FirstPersonController MainFPS;
    public Transform CameraJoint;
    public bool started;
    public InputSystem_Actions MainInput;
    PlayableDirector PlayableDirector;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] AudioMixer mixer;
    [SerializeField] float GameVolume;
    [SerializeField] PlayableDirector StartBedAnimator;
    [SerializeField] GameObject PostDeathUI;
    public CinemachineBrain Camera;
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

    }
    private void Start()
    {
        StartBedAnimator.Play();

    }

    private void FixedUpdate()
    {
        mixer.SetFloat("GameVolume", GameVolume);
    }
    public void StartGame()
    {
        
        if (started == false)
        {
            started = true;
            
            MainFPS.playerCanMove = true;
            MainFPS.cameraCanMove = true;
            MainFPS.enableSprint = true;
        }
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
        GameVolume = -80;
        PostDeathUI.SetActive(true);

    }
}
