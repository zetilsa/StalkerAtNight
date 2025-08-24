using UnityEngine;
using DG.Tweening;
using UnityEditor.ShaderGraph.Serialization;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform[] paths;
    [SerializeField] private GameObject[] UI;
    [SerializeField] private CanvasGroup cg;

    private bool onClock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Move(int state)
    {
        cg.blocksRaycasts = false;
        if (state != 1 || state != 2 || state != 7)
        {
            transform.DOMove(paths[state].position, .5f).SetEase(Ease.InOutCubic);
        }
        if (state == 7)
        {
            if (onClock == false)
            {
                onClock = true;
                DOTween.Kill(transform);
                transform.DORotateQuaternion(paths[state].rotation, .5f).SetEase(Ease.OutCubic);
            }
        }
        else if(state != 7)
        {

                onClock = false;
                transform.DORotateQuaternion(paths[state].rotation, .5f).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    foreach (GameObject ui in UI)
                    {
                        ui.SetActive(false);
                    }
                    UI[state].SetActive(true);
                    cg.blocksRaycasts = true;
                });
        }
    }
}
