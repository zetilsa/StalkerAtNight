using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
public class ClosetManager : MonoBehaviour
{
    public static ClosetManager instance { get; private set; }
    public bool Hidestate;
    [SerializeField] PlayableDirector PD;
    [SerializeField] MultiTimelineAsset MTA;
    [SerializeField] ClosetProperties CP;
    Transform ClosetInitPoint;
    [SerializeField] Transform CamPoint;
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
        
        GameManager.instance.MainFPS.CameraJoint.DOMove(ClosetInitPoint.position, .5f).OnComplete(() =>
        {
            PD.Play();
            PD.Evaluate();
            GameManager.instance.MainFPS.CameraJoint.GetComponent<LockCameraTransform>().Target = CamPoint;
            GameManager.instance.MainFPS.CameraJoint.GetComponent<LockCameraTransform>().enabled = true;
            
        });
        GameManager.instance.MainFPS.CameraJoint.DORotateQuaternion(ClosetInitPoint.rotation, .5f);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
