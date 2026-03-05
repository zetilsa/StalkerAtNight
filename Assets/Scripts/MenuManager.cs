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
