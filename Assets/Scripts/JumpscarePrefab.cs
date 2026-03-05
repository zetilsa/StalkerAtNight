using UnityEngine;

public class JumpscarePrefab : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PostAnim()
    {
        GameManager.instance.PostJumpscare();
    }
    public void CleanUp()
    {
        GameManager.instance.MainFPS.gameObject.SetActive(false);
    }
}
