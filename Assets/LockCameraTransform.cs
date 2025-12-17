using DG.Tweening;
using UnityEngine;

public class LockCameraTransform : MonoBehaviour
{

    public Transform Target;
    [SerializeField] Transform PlayerPoint;
    private void OnEnable()
    {
        InvokeRepeating("Lock", 0, 0.001f);
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
        if (PlayerManager.instance.IsHiding == false)
        {
            transform.DOLocalMove(PlayerPoint.localPosition, .5f);
        }
    }
}
