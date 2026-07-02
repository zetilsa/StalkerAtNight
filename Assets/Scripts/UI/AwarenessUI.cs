using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AwarenessUI : MonoBehaviour
{
    float awareness;
    [SerializeField] Gradient colors;
    [SerializeField] Transform Eye;
    [SerializeField] Image OutterEye;
    [SerializeField] Image Pupil;
    [SerializeField] TextMeshProUGUI Meter;
    [SerializeField] CanvasGroup Guide;

    [SerializeField] bool EnableTutorialGuide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Meter.text = $"{Mathf.Round(awareness)}%";    
    }

    private void FixedUpdate()
    {
        awareness = PlayerManager.instance.Awareness;
        OutterEye.color = colors.Evaluate(awareness / 100);
        Pupil.color = colors.Evaluate(awareness / 100);
        Meter.color = colors.Evaluate(awareness / 100);


        if(EnableTutorialGuide == true)
        {
            if(awareness < 90)
            {
                EnableTutorialGuide = false;
                Guide.DOFade(1, 1).OnComplete(() =>
                {
                    Guide.DOFade(0.5f, 1).OnComplete(() =>
                    {
                        Guide.DOFade(1, 1).OnComplete(() =>
                        {
                            Guide.DOFade(0.5f, 1).OnComplete(() =>
                            {
                                Guide.DOFade(1, 1).OnComplete(() =>
                                {
                                    Guide.DOFade(0.5f, 1).OnComplete(() =>
                                    {
                                        Guide.DOFade(1, 1).OnComplete(() =>
                                        {
                                            Guide.DOFade(0, 1);
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            }
        }
    }
}
