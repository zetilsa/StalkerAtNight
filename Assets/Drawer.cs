using UnityEngine;
using DG.Tweening;
public class Drawer : MonoBehaviour
{
    [SerializeField] float Min;
    [SerializeField] float Max;
    [SerializeField] float time;
    bool state;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Interact()
    {
        state = !state;

        if(state == false)
        {
            transform.DOLocalMoveX(Min, time).SetEase(Ease.InOutCubic);
        }
    else if (state == true)
        {
            transform.DOLocalMoveX(Max, time).SetEase(Ease.InOutCubic);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
