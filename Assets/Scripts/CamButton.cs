using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CamButton : MonoBehaviour
{
    [SerializeField] int cameraID;
    public GameObject Fill;
    public GameObject Camera;
    public bool isGlitching;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateGlitch(float GlitchTime)
    {
        isGlitching = true;
        if (CCTVManager.instance.currentCam == cameraID)
        {
            CCTVManager.instance.Glitch();
        }
        IEnumerator i()
        {
            yield return new WaitForSeconds(GlitchTime);
            isGlitching = false;
            if (PlayerManager.instance.OnComputer == true)
            {
                CCTVManager.instance.ChangeCam(CCTVManager.instance.currentCam); //Refresh
            }
        }
        StartCoroutine(i());
    }

    public void OnClick()
    {
        if (!isGlitching)
        {
            Fill.SetActive(!Fill.activeInHierarchy);
        }
        CCTVManager.instance.ChangeCam(cameraID);
    }
}
