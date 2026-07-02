using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {  get; private set; }

    [SerializeField] Rigidbody rb;
    [SerializeField] FirstPersonController FPS;

    public bool OnBed;
    public bool OnComputer;
    public bool IsHiding;
    public bool Transition;

    public bool enableHoldBreath;
    public float breath;
    float breathvalue;
    [SerializeField] Vector2 breathmodifierRate;
    float breathmodifier;
    public bool recoverbreath;

    public float Awareness;
    public bool recoverawareness;
    [SerializeField] Vector2 awarenessmodifierRate;
    [SerializeField] CanvasGroup AwarenessBarUI;
    [SerializeField] Image AwarenessBarUIFill;
    public GameObject BedCamera;
    [SerializeField] CanvasGroup BreathBarUI;
    [SerializeField] Image BreathBarUIFill;
    bool BreathUIShowed;

    [SerializeField] AudioSource AudioSrc;
    [SerializeField] AudioClip[] PlayerSfx;
    bool Mechanic1;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameManager.instance.MainInput.Player.Interact.performed += Interact;
        GameManager.instance.MainInput.Player.Interact.Enable();
    }
    private void Update()
    {
        if (enableHoldBreath == true)
        {
            HoldBreath();
        }
        else
        {
            recoverbreath = true;
        }
    }
    private void FixedUpdate()
    {
        /*if (Awareness == 100 && AwarenessBarUI.alpha == 1)
        {
            AwarenessBarUI.DOFade(0, 1);
        }
        else if (Awareness < 100 && AwarenessBarUI.alpha == 0)
        {
            AwarenessBarUI.DOFade(1, 1);
        }*/
        if (recoverawareness == true)
        {
            Awareness = Mathf.Clamp(Awareness + awarenessmodifierRate.y, 0, 100);
        }
        else if(recoverawareness == false)
        {
            if(Awareness == 0 && GameManager.instance.started == true)
            {
                print("GameOver");
                GameManager.instance.started = false;

                AudioSrc.clip = PlayerSfx[0];
                AudioSrc.Play();
                GameManager.instance.GameOver();
            }
            Awareness = Mathf.Clamp(Awareness + awarenessmodifierRate.x, 0, 100);
        }
            if (breath != 100 && BreathUIShowed == false)
        {
            BreathUIShowed = true;
            BreathBarUI.DOFade(1, 1);
        }
        else if (breath == 100 && BreathUIShowed == true)
        {
            BreathUIShowed = false;
            BreathBarUI.DOFade(0, 1);
        }
        
        
            breathvalue = Mathf.Clamp(breathvalue + breathmodifier, 0, 100);
        breath = Mathf.Round(breathvalue);
        BreathBarUIFill.fillAmount = breathvalue / 100;
        //AwarenessBarUIFill.fillAmount = Awareness / 100;
        if (recoverbreath == true)
        {
            breathmodifier = breathmodifierRate.y;

        }
        else if (recoverbreath == false)
        {
            breathmodifier = breathmodifierRate.x;
        }
    }
    void HoldBreath()
    {
        if (Input.GetButtonDown("Mechanic1"))
        {
            recoverbreath = false;

            GameManager.instance.BreathingSFX.mute = true;
        }
        else if (Input.GetButtonUp("Mechanic1"))
        {
            recoverbreath = true;
            GameManager.instance.BreathingSFX.mute = false;
        }

        if (breath == 0 && GameManager.instance.started == true)
        {
            print("GameOver");
            GameManager.instance.started = false;

            AudioSrc.clip = PlayerSfx[0];
            AudioSrc.Play();
            GameManager.instance.GameOver();
        }

    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {

    }
    void Interact(InputAction.CallbackContext context)
    {
        if (IsHiding == true && Transition == false && RaycastManager.Instance.EnableRaycast == true)
        {

                Transition = true;
                print("CloseHide");
                ClosetManager.instance.Hide(false);

        }
    }

    public void DoSomething(string id)
    {
        if (id == "Hide")
        {
            IsHiding = !IsHiding;
            if(IsHiding == true)
            {
                GameManager.instance.HeartBeatSFX.mute = false;
                GameManager.instance.BreathingSFX.mute = false;
            }
            else if(IsHiding == false)
            {
                GameManager.instance.HeartBeatSFX.mute = true;
                GameManager.instance.BreathingSFX.mute = true;
            }
                Transition = false;

        }

        else if(id == "Sleep")
        {
            recoverawareness = true;
        }
        else if(id == "UnSleep")
        {
            recoverawareness = false;
        }
    }


    public void ChangeControlState(bool MainInput,bool PlayerCanMove,bool CameraCanMove,bool EnableSprint,bool EnableRaycast,bool SetVelocityToZero,bool cursorvisibility,bool cursorlock,bool CameraRotationMode)
    {

        if (SetVelocityToZero)
        {
            rb.linearVelocity = Vector3.zero;
        }
        if (MainInput == true)
        {
            GameManager.instance.MainInput.Player.Enable();
        }
        else if (MainInput == true)
        {
            GameManager.instance.MainInput.Player.Disable();
        }
        //GetComponent<FirstPersonController>().LocalRotationCamMode = CameraRotationMode;
        FPS.playerCanMove = PlayerCanMove;
        FPS.cameraCanMove = CameraCanMove;
        FPS.enableSprint = EnableSprint;
        RaycastManager.Instance.EnableRaycast = EnableRaycast;


        if (cursorvisibility == true)
        {
            Cursor.visible = true;
        }
        else if (cursorvisibility == false)
        {
            Cursor.visible = false;
        }

        if(cursorlock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(cursorlock == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }


    }

    public void ChangeMechanicControlState(int ControlType)
    {
        switch (ControlType)
        {
            case 0:
                Mechanic1 = !Mechanic1;
                if (Mechanic1 == true)
                {
                    GameManager.instance.MainInput.Mechanic.Mechanic1.Enable();
                }
                else if (Mechanic1 == false)
                {
                    GameManager.instance.MainInput.Mechanic.Mechanic1.Disable();
                }
                break;
            case 1:
                enableHoldBreath = !enableHoldBreath;
                break;
        }

    }

    public void ChangeMechanicControlState(int ControlType,bool value)
    {
        switch (ControlType)
        {
            case 0:
                Mechanic1 = value;
                if (Mechanic1 == true)
                {
                    GameManager.instance.MainInput.Mechanic.Mechanic1.Enable();
                }
                else if (Mechanic1 == false)
                {
                    GameManager.instance.MainInput.Mechanic.Mechanic1.Disable();
                }
                break;
            case 1:
                enableHoldBreath = value;
                break;
        }

    }

    public void SetControl()
    {
        ChangeControlState(true,true,true,true,true,false, false, true,false);
    }
}


