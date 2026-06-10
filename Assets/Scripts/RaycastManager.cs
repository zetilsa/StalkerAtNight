using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEngine.Animations;

using UnityEngine.InputSystem;
public class RaycastManager : MonoBehaviour
{
    public static RaycastManager Instance { get; private set; }
    public float currentRaycastDistance;
    public int maxDistance;
    public LayerMask layerMask;
    public LayerMask layermaskexclude;
    Camera mainCam;
    RaycastHit[] raycastHits = new RaycastHit[1];
    GameObject Selected;
    GameObject tempselect;

    GameManager GMinst;
    PathManager PMgr;
    PlayerManager PlayerMgr;

    //Controller
    [SerializeField] OnComputerControl OCC;

    private bool OnComputer;
    private bool onTransition;

    public bool EnableRaycast;
    bool raycastfirsttime;

    string[] RaycastTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        
        GMinst = GameManager.instance;
        PMgr = PathManager.instance;
        PlayerMgr = PlayerManager.instance;
        mainCam = GMinst.MainFPS.playerCamera;

        //Controller

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
            PlayerMgr.ChangeControlState(false,false,false,false,false,true, true,false,false);

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
        CCTVManager.instance.OnUnUse();
        EyeViewManager.Instance.Blink(0.25f);
        yield return new WaitForSeconds(0.25f);
        if (PlayerMgr.OnComputer == true)
        {
            PlayerMgr.OnComputer = false;
            PlayerMgr.ChangeControlState(true,true,true,true,true,false,false,true,false);
            CrosshairManager.instance.SetShow(true);
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
        RaycastHit hitray = new RaycastHit();
        if (Physics.Raycast(ray, out hitray, 100,layermaskexclude))
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 hitPoint = hitray.point;

            currentRaycastDistance = Vector3.Distance(cameraPos, hitPoint);
            if (EnableRaycast == false) return;

            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                //set Selected Object for first time and check if the hit target was interactable, and then call CrosshairManager to change crosshair state
                if (Selected != hit.collider.gameObject)
                {
                    Selected = hit.collider.gameObject;
                    if (Selected.TryGetComponent<TagSystemAkeh>(out TagSystemAkeh tagsyst))
                    {
                        RaycastTag = tagsyst.tag;
                        foreach (string tag in RaycastTag)
                        {
                            if (tag == "Interactable" && raycastfirsttime == false)
                            {
                                raycastfirsttime = true;
                                CrosshairManager.instance.SetDetect(true);
                            }
                            else if (tag != "Interactable" && raycastfirsttime == false)
                            {
                                raycastfirsttime = true;
                                CrosshairManager.instance.SetDetect(false);
                            }
                            switch (tag)
                            {
                                case "Interactable":
                                    CrosshairManager.instance.SetDetect(true);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        CrosshairManager.instance.SetDetect(false);
                    }
                }

                //checking any interactable hit object by checking TagSystemAkeh
                if (Selected.TryGetComponent<TagSystemAkeh>(out TagSystemAkeh tagSystemAkeh))
                {
                    RaycastTag = tagSystemAkeh.tag;
                    foreach (string tag in RaycastTag)
                    {
                        switch (tag)
                        {
                            case "Bed":
                                if (GMinst.MainInput.Player.Interact.triggered)
                                {
                                    GMinst.SetCameraBlendValue(0);
                                    StartCoroutine(EnterBed());
                                }
                                break;
                            case "Curtain":
                                print("here curtain");
                                if (Input.GetMouseButtonDown(0))
                                {
                                    Selected.GetComponent<Animator>().SetTrigger("Open");
                                    PlayerMgr.ChangeControlState(true,false,false,false,true,true,false,true,false);

                                } else if (Input.GetMouseButtonUp(0))
                                {
                                    Selected.GetComponent<Animator>().SetTrigger("Close");
                                    PlayerMgr.ChangeControlState(true,true,true,true,true,false, false, true,false);
                                }
                                    break;
                             
                            case "Drawer":
                                if (Input.GetMouseButton(0))
                                {
                                    Selected.GetComponent<Drawer>().Interact(true);
                                    GMinst.MainFPS.cameraCanMove = false;
                                }
                                else
                                if (Input.GetMouseButtonUp(0))
                                {
                                    print("debugdrawer");
                                    GMinst.MainFPS.cameraCanMove = true;
                                }
                                break;
                            case "DrawerV2":
                                if (Input.GetMouseButtonDown(0))
                                {
                                    Selected.GetComponent<Drawer>().InteractV2();
                                }
                                break;
                            case "LightSwitch":
                                if (GMinst.MainInput.Player.Interact.triggered && PlayerManager.instance.Transition == false && onTransition == false)
                                {
                                    Selected.GetComponent<MainLightSwitch>().Interact();
                                }
                                    break;
                            case "Door":
                                if (GMinst.MainInput.Player.Interact.triggered && PlayerManager.instance.Transition == false && onTransition == false)
                                {
                                    Selected.GetComponent<InteractablePintu>().Interact();
                                }
                                break;
                            case "PC":
                                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                {
                                    
                                    StartCoroutine(EnterPC());
                                }
                                break;

                            case "Closet":
                                /*foreach (var output in Selected.GetComponent<PlayableDirector>().playableAsset.outputs)
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
                                */
                                if (Selected.GetComponent<ClosetManager>().Hidestate == false)
                                {
                                    if (GMinst.MainInput.Player.Interact.triggered && PlayerManager.instance.Transition == false && onTransition == false)
                                    {
                                        GuideTextManager.instance.SetText(new string[2] { "[LMB] to Get Out", "Hold [RMB] to Hold Breath" });
                                        PlayerMgr.ChangeControlState(true,false,false,false,true,true,false, true,true);
                                        Selected.GetComponent<ClosetManager>().Hide(true);
                                    }
                                }
                                break;


                        }
                    }
                }
                else
                {
                    CrosshairManager.instance.SetDetect(false);
                }

            }
            else
            {
                Selected = null;
                CrosshairManager.instance.SetDetect(false);
            }
        }

    }


    IEnumerator EnterPC()
    {
        EyeViewManager.Instance.Blink(0.25f);
        yield return new WaitForSeconds(0.25f);
        if (PlayerMgr.OnComputer == false)
        {

            PlayerMgr.OnComputer = true;
            PlayerMgr.ChangeControlState(false,false,false,false,false,true,true,false,false);
            CrosshairManager.instance.SetShow(false);

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

            CCTVManager.instance.OnUse();
        }

    }

    IEnumerator EnterBed()
    {
        EyeViewManager.Instance.Blink(0.25f);
        yield return new WaitForSeconds(0.25f);
        if (PlayerMgr.OnBed == false)
        {
            PlayerMgr.OnBed = true;
            PlayerMgr.ChangeControlState(false, false, false, false, false, true, false, true, false);
            CrosshairManager.instance.SetShow(false);
            
            PlayerMgr.BedCamera.SetActive(true);
            PlayerMgr.DoSomething("Sleep");
            
        }

    }





    void closetplayabledirectorstopped(PlayableDirector director)
    {
        onTransition = false;
        CancelInvoke();
    }
}
