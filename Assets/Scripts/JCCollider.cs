using UnityEngine;

public class JCCollider : MonoBehaviour
{
    [SerializeField] JCRay jc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        jc.collided = true;
    }
    private void OnTriggerExit(Collider other)
    {
        jc.collided = false;
    }
}
