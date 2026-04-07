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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int SceneID)
    {
        IEnumerator w()
        {
            GameSystem.instance.SetVolume(0, 1);
            FadeManager.instance.Fade(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneID);
        }
        StartCoroutine(w());
    }
}
