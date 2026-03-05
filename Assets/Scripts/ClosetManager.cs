using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using Unity.Cinemachine;
public class ClosetManager : MonoBehaviour
{
    public static ClosetManager instance { get; private set; }
    public bool Hidestate;
    [SerializeField] PlayableDirector PD;
    [SerializeField] MultiTimelineAsset MTA;
    [SerializeField] ClosetProperties CP;
    Transform ClosetInitPoint;
    [SerializeField] Transform CamPoint;
    [SerializeField] CinemachineCamera CameraPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void Hide()
    {
        Hidestate = !Hidestate;

        if(Hidestate == true)
        {
            PD.playableAsset = MTA.timelineAssets[0];
            
        }
        else if(Hidestate == false)
        {
            PD.playableAsset = MTA.timelineAssets[1];
        }
        PD.Play();
    }

    public void Hide(bool state)
    {
        Hidestate = state;
        PD.Stop();
        if (state == true)
        {
            print("elekan");
            PD.playableAsset = MTA.timelineAssets[0];
            ClosetInitPoint = CP.CameraPoint[0];
        }
        else if (state == false)
        {
            print("nigger");
            PD.playableAsset = MTA.timelineAssets[1];
            ClosetInitPoint = CP.CameraPoint[1];
        }
        GameManager.instance.SetCameraBlendValue(0.5f);
        PD.Play();
        PD.Evaluate();
        CameraPoint.enabled = true;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
