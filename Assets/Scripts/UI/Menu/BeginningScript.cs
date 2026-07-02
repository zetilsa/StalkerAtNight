using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class BeginningScript : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] CanvasGroup Title;
    [SerializeField] CanvasGroup MainMenu;

    [SerializeField] CinemachineCamera cinemachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        cg.blocksRaycasts = false;
        cg.DOFade(0, 1).OnComplete(() =>
        {
            cinemachine.Prioritize();
            Title.DOFade(1, 1);
            MainMenu.DOFade(1, 1).OnComplete(() =>
            {
                MainMenu.blocksRaycasts = true;
            });
        });
    }
}
