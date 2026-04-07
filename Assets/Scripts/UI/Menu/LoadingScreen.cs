using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
