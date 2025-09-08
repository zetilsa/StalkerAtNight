using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public FirstPersonController MainFPS;
    public Transform CameraJoint;
    bool started;

    PlayableDirector PlayableDirector;
    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        GameObject.Find("Bed").GetComponent<PlayableDirector>().Play();
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
}
