using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Security.Policy;
public class GuideTextManager : MonoBehaviour
{
    public static GuideTextManager instance { get; private set; }
    [SerializeField]TextMeshProUGUI[] Guides;
    [SerializeField]CanvasGroup cg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string[] Texts)
    {
        foreach (TextMeshProUGUI guide in Guides)
        {
            guide.text = "";
        }
        for (int i = 0; i < Texts.Length; i++)
        {
            Guides[i].text = Texts[i];
            Guides[i].GetComponent<LocalizedText>().SetKey(Texts[i]);
        }
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.Refresh();
        }
    }

    public void Show()
    {
        cg.DOFade(1, 1);
    }
    public void Show(float time)
    {
        cg.DOFade(1, time);
    }


    public void Hide()
    {
        cg.DOFade(0, 1);
    }
    public void Hide(float time)
    {
        cg.DOFade(0, time);
    }
}
