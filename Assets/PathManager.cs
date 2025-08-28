using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance {  get; private set; }

    public Transform[] Points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
