using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class MapUIHover : MonoBehaviour
{
    [SerializeField] Vector3[] pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover(bool state)
    {
        if (state == true)
        {
            transform.DOLocalMove(pos[0],.5f).SetEase(Ease.OutCubic);
        }
        else if(state == false)
        {
            transform.DOLocalMove(pos[1], .5f).SetEase(Ease.OutCubic);
        }
    }
}
