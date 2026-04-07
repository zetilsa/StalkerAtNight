using DG.Tweening;
using UnityEngine;

public class MenuUITransitionHandler : MonoBehaviour
{
    [SerializeField]CanvasGroup current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MulaiTransisiKece(CanvasGroup target)
    {
        target.transform.localScale = new Vector3(.8f, .8f, .8f);
        target.transform.DOScale(1, 1).SetEase(Ease.OutCubic);
        target.DOFade(1, 1).SetEase(Ease.OutCubic);

        current.transform.DOLocalMoveX(600, .75f);
        current.transform.DOScale(1.2f, 1).SetEase(Ease.OutCubic);
        current.DOFade(0,.75f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            current.transform.localPosition = Vector3.zero;
            current.blocksRaycasts = false;
            current = target;
            target.blocksRaycasts = true;
        }); 
    }

    public void MulaiTransisiKeceCredits(CanvasGroup target)
    {
        target.blocksRaycasts = true;

        target.DOFade(1, 1).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            target.GetComponent<AutoCreditsScript>().Aktif();
        });
    }
}
