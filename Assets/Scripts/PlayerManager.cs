using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {  get; private set; }

    public bool OnComputer;
    public bool IsHiding;
    public bool Transition;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        GameManager.instance.MainInput.Player.Interact.performed += Interact;
        GameManager.instance.MainInput.Player.Interact.Enable();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        GameManager.instance.MainInput.Player.Interact.performed -= Interact;
        GameManager.instance.MainInput.Player.Interact.Disable();
    }
    void Interact(InputAction.CallbackContext context)
    {
        if (IsHiding == true && Transition == false)
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
            Transition = false;

        }
    }


    public void ChangeControlState(int state)
    {
        print("called");
        if (state == 0)
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GetComponent<FirstPersonController>().playerCanMove = false;
            GetComponent<FirstPersonController>().cameraCanMove = false;
            GetComponent<FirstPersonController>().enableSprint = false;
            RaycastManager.Instance.EnableRaycast = false;
        }
        else if (state == 1)
        {
            GetComponent<FirstPersonController>().playerCanMove = true;
            GetComponent<FirstPersonController>().cameraCanMove = true;
            GetComponent<FirstPersonController>().enableSprint = true;
            RaycastManager.Instance.EnableRaycast = true;
        }
    }

}
