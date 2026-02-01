using UnityEngine;
using UnityEngine.UI;
public class CamButton : MonoBehaviour
{
    [SerializeField] int cameraID;
    public GameObject Fill;
    public GameObject Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Fill.SetActive(!Fill.activeInHierarchy);
        CCTVManager.instance.ChangeCam(cameraID);
    }
}
