using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public class READPLS : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cg.DOFade(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mulai()
    {
        cg.DOFade(0, 1).OnComplete(() =>
        {
            SceneManager.LoadScene(2);
        });
    }
}
