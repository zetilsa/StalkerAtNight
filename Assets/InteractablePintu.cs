using DG.Tweening;
using UnityEngine;

public class InteractablePintu : MonoBehaviour
{
    [SerializeField] float Offval;
    [SerializeField] float Onval;
    [SerializeField] float time;
    bool state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Interact()
    {
        state = !state;
        if(state == true)
        {
            transform.DOLocalRotate(new Vector3(-90, Onval,0), time).SetEase(Ease.InOutCubic);
        }
        else
        if (state == false)
        {
            transform.DOLocalRotate(new Vector3(-90, Offval,0), time).SetEase(Ease.InOutCubic);
        }
    }
}
