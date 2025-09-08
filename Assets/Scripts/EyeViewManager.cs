using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class EyeViewManager : MonoBehaviour
{
    public static EyeViewManager Instance { get; private set; }
    [SerializeField] RectTransform[] view;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame

    public void BlinkEvent(float seconds)
    {
        print(view[0].name);
        view[0].DOLocalMoveY(20, 0.25f);
        view[1].DOLocalMoveY(-20, 0.25f).OnComplete(() =>
        {
            GameEvents.Instance.TriggerFirstTransitionComplete();

            view[0].DOLocalMoveY(1080, 0.25f);
            view[1].DOLocalMoveY(-1080, 0.25f).OnComplete(() =>
            {

            });
        });
    }


    public void Blink(float seconds)
    {
        print(view[0].name);
        view[0].DOLocalMoveY(20, 0.25f);
        view[1].DOLocalMoveY(-20, 0.25f).OnComplete(() =>
        {
            view[0].DOLocalMoveY(1080, 0.25f);
            view[1].DOLocalMoveY(-1080, 0.25f).OnComplete(() =>
            {

            });
        });
    }
}
