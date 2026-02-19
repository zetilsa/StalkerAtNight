using UnityEngine;
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

    [SerializeField] PlayableDirector StartBedAnimator;
    void OnEnable()
    {
        MainInput = new InputSystem_Actions();

        if (instance == null)
        {
            instance = this;
        }

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
        MainFPS.CameraJoint.GetComponent<LockCameraTransform>().enabled = true;
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
}
