using UnityEngine;

public class LockCameraTransform : MonoBehaviour
{

    public Transform Target;

    private void OnEnable()
    {
        InvokeRepeating("Lock", 0, 0.01f);
    }

    private void OnDisable()
    {
        CancelInvoke("Lock");
    }

    void Lock()
    {
        transform.position = Target.position;
        transform.rotation = Target.rotation;
    }

    public void Disable()
    {
        enabled = false;
    }
}
