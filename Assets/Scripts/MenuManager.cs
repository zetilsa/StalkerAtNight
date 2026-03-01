using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mixer.SetFloat("GameVolume", -80);
        Mixer.DOSetFloat("GameVolume", 0, 2).SetEase(Ease.OutCubic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int SceneID)
    {
        IEnumerator w()
        {
            Mixer.DOSetFloat("GameVolume", -80, 1).SetEase(Ease.InCubic);
            FadeManager.instance.Fade(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneID);
        }
        StartCoroutine(w());
    }
}
