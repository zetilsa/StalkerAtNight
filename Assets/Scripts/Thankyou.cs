using UnityEngine;
using UnityEngine.SceneManagement;

public class Thankyou : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.Reset();
        }
    }

    public void ChangeMenu()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
