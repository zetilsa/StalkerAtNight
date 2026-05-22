using UnityEngine;
using UnityEngine.UI;

public class AwarenessUI : MonoBehaviour
{
    float awareness;
    [SerializeField] Gradient colors;
    [SerializeField] Transform Eye;
    [SerializeField] Image OutterEye;
    [SerializeField] Image Pupil;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        awareness = PlayerManager.instance.Awareness;
        OutterEye.color = colors.Evaluate(awareness / 100);
        Pupil.color = colors.Evaluate(awareness / 100);
    }
}
