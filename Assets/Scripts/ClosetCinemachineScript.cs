using Unity.Cinemachine;
using UnityEngine;

public class ClosetCinemachineScript : MonoBehaviour
{
    [SerializeField] CinemachinePanTilt CPT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool state)
    {
        CPT.enabled = state;
    }
}
