using UnityEngine;
using DG.Tweening;
using UnityEditor.ShaderGraph.Serialization;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform[] paths;
    [SerializeField] private GameObject[] UI;
    [SerializeField] private CanvasGroup cg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(int state)
    {
        cg.blocksRaycasts = false;
        if (state == 0)
        {
            transform.DOMove(paths[0].position, .5f).SetEase(Ease.InOutCubic);
            transform.DORotateQuaternion(paths[0].rotation, .5f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                UI[0].SetActive(true);
                UI[1].SetActive(false);
                UI[2].SetActive(false);
                UI[3].SetActive(false);
                cg.blocksRaycasts = true;
            });
        }
        else if (state == -1)
        {
            transform.DORotateQuaternion(paths[2].rotation, .5f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                UI[0].SetActive(false);
                UI[1].SetActive(true);
                UI[2].SetActive(false);
                UI[3].SetActive(false);
                cg.blocksRaycasts = true;
            });
        }
        else if (state == 1)
        {
            transform.DORotateQuaternion(paths[1].rotation, .5f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                UI[0].SetActive(false);
                UI[1].SetActive(false);
                UI[2].SetActive(true);
                UI[3].SetActive(false);
                cg.blocksRaycasts = true;
            });
        }
        else if (state == 2)
        {
            transform.DOMove(paths[3].position, .5f).SetEase(Ease.InOutCubic);
            transform.DORotateQuaternion(paths[3].rotation, .5f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                UI[0].SetActive(false);
                UI[1].SetActive(false);
                UI[2].SetActive(false);
                UI[3].SetActive(true);
                cg.blocksRaycasts = true;
            });
        }
        else
        {
            cg.blocksRaycasts = true;
        }
    }
}
