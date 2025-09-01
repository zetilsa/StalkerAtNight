using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class RaycastManager : MonoBehaviour
{
    public static RaycastManager Instance { get; private set; }

    public int maxDistance;
    public LayerMask layerMask;
    Camera mainCam;
    RaycastHit[] raycastHits = new RaycastHit[1];
    GameObject Selected;

    GameManager GMinst;
    PathManager PMgr;
    PlayerManager PlayerMgr;

    //Controller
    OnComputerControl OCC;

    private bool OnComputer;
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
            GMinst.MainFPS.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GMinst.MainFPS.playerCanMove = false;
            GMinst.MainFPS.cameraCanMove = false;
            GMinst.MainFPS.enableSprint = false;

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
            GMinst.MainFPS.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GMinst.MainFPS.playerCanMove = true;
            GMinst.MainFPS.cameraCanMove = true;
            GMinst.MainFPS.enableSprint = true;

            GMinst.MainFPS.playerCamera.transform.position = new Vector3(0, 0.3f, 0) + GMinst.CameraJoint.position;
            GMinst.MainFPS.playerCamera.transform.rotation = GMinst.CameraJoint.rotation;
        }
    }
        
        void RaycastHitUpdate()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            
            switch (hit.collider.tag)
            {
                case "PC":
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(EnterPC());
                    }
                    break;

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
            GMinst.MainFPS.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GMinst.MainFPS.playerCanMove = false;
            GMinst.MainFPS.cameraCanMove = false;
            GMinst.MainFPS.enableSprint = false;

            GMinst.MainFPS.playerCamera.transform.position = PMgr.Points[0].position;
            GMinst.MainFPS.playerCamera.transform.rotation = PMgr.Points[0].rotation;
        }

    }
}
