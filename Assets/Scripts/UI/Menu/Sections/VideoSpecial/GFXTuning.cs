using UnityEngine;

public class GFXTuning : MonoBehaviour
{
    [SerializeField] SelectionOption GFXOption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        QualitySettings.SetQualityLevel(GFXOption.value);

    }
}
