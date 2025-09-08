using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.Animations;
public class RaycastManager : MonoBehaviour
{
    public static RaycastManager Instance { get; private set; }

    public int maxDistance;
    public LayerMask layerMask;
    Camera mainCam;
    RaycastHit[] raycastHits = new RaycastHit[1];
    GameObject Selected;
    GameObject tempselect;

    GameManager GMinst;
    PathManager PMgr;
    PlayerManager PlayerMgr;

    //Controller
    OnComputerControl OCC;

    private bool OnComputer;
    private bool onTransition;



    string[] RaycastTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        mainCam = GetComponent<Camera>();
        GMinst = GameManager.instance;
        PMgr = PathManager.instance;
        PlayerMgr = PlayerManager.instance;


        //Controller
        OCC = GetComponent<OnComputerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHitUpdate();
        ControllerUpdate();
    }

    void OnEnable()
    {

        
        
    }

    void ControllerUpdate()
    {
        if(OCC.enabled != PlayerMgr.OnComputer)
        {
            OCC.enabled = PlayerMgr.OnComputer;
        }
    }
    public void ProcessTransitionEvent()
    {
        print(PlayerMgr.OnComputer);
        if (PlayerMgr.OnComputer == false)
        {
            PlayerMgr.OnComputer = true;
            PlayerMgr.ChangeControlState(0);

            GMinst.MainFPS.playerCamera.transform.position = PMgr.Points[0].position;
            GMinst.MainFPS.playerCamera.transform.rotation = PMgr.Points[0].rotation;
        }
        
        
    }
    
    public void ExitCamera()
    {
        StartCoroutine(exitcam());
    }
    IEnumerator exitcam()
    {
        EyeViewManager.Instance.Blink(0.25f);
        yield return new WaitForSeconds(0.25f);
        if (PlayerMgr.OnComputer == true)
        {
            PlayerMgr.OnComputer = false;
            PlayerMgr.ChangeControlState(1);

            if (GMinst.MainFPS.useCinemachine == false)
            {
                GMinst.MainFPS.playerCamera.transform.position = new Vector3(0, 0.3f, 0) + GMinst.CameraJoint.position;
                GMinst.MainFPS.playerCamera.transform.rotation = GMinst.CameraJoint.rotation;
            }
            else
            {
                GMinst.MainFPS.CameraJoint.transform.position = new Vector3(0, 0.3f, 0) + GMinst.CameraJoint.position;
                GMinst.MainFPS.CameraJoint.transform.rotation = GMinst.CameraJoint.rotation;
            }
        }
    }
        
        void RaycastHitUpdate()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            Selected = hit.collider.gameObject;
            RaycastTag = Selected.GetComponent<TagSystemAkeh>().tag;
            try
            {
                foreach (string tag in RaycastTag) {
                    switch (tag)
                    {
                        case "PC":
                            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                            {
                                StartCoroutine(EnterPC());
                            }
                            break;

                        case "Closet":
                            foreach (var output in Selected.GetComponent<PlayableDirector>().playableAsset.outputs)
                            {

                                if (output.streamName == "Animation Track (1)")
                                {
                                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) && PlayerManager.instance.Transition == false && onTransition == false)
                                    {
                                        // Bind the object to this track
                                        PlayerMgr.ChangeControlState(0);
                                        tempselect = Selected;

                                        if (PlayerMgr.IsHiding == true)
                                        {
                                            onTransition = true;
                                            
                                            PlayerManager.instance.Transition = true;
                                            GMinst.MainFPS.CameraJoint.GetComponent<LockCameraTransform>().enabled = true;

                                            tempselect.GetComponent<PlayableDirector>().playableAsset = tempselect.GetComponent<MultiTimelineAsset>().timelineAssets[1];
                                            tempselect.GetComponent<PlayableDirector>().Play();

                                        }
                                        else if (PlayerMgr.IsHiding == false)
                                        {
                                            GMinst.MainFPS.CameraJoint.DOMove(tempselect.GetComponent<ClosetProperties>().CameraPoint.position, .3f).OnComplete(() =>
                                            {
                                                onTransition = true;
                                                PlayerManager.instance.Transition = true;
                                                GMinst.MainFPS.CameraJoint.GetComponent<LockCameraTransform>().enabled = true;
                                            });
                                            GMinst.MainFPS.CameraJoint.DORotateQuaternion(tempselect.GetComponent<ClosetProperties>().CameraPoint.rotation, .25f).OnComplete(() =>
                                            {
                                                tempselect.GetComponent<PlayableDirector>().playableAsset = tempselect.GetComponent<MultiTimelineAsset>().timelineAssets[0];
                                                tempselect.GetComponent<PlayableDirector>().Play();

                                            });
                                        }




                                    }
                                }
                            }
                            break;

                    }
                }
            }
            finally
            {

            }

        }
        else
        {
            Selected = null;
        }
    }


    IEnumerator EnterPC()
    {
        EyeViewManager.Instance.Blink(0.25f);
        yield return new WaitForSeconds(0.25f);
        if (PlayerMgr.OnComputer == false)
        {
            PlayerMgr.OnComputer = true;
            PlayerMgr.ChangeControlState(0);

            if (GMinst.MainFPS.useCinemachine == false)
            {
                GMinst.MainFPS.playerCamera.transform.position = PMgr.Points[0].position;
                GMinst.MainFPS.playerCamera.transform.rotation = PMgr.Points[0].rotation;
            }
            else
            {
                GMinst.MainFPS.CameraJoint.transform.position = PMgr.Points[0].position;
                GMinst.MainFPS.CameraJoint.transform.rotation = PMgr.Points[0].rotation;
            }
        }

    }







    void closetplayabledirectorstopped(PlayableDirector director)
    {
        onTransition = false;
        CancelInvoke();
    }
}
