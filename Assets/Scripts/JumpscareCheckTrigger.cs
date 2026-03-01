using UnityEngine;

public class JumpscareCheckTrigger : MonoBehaviour
{
    public bool state;
    public Vector2[] Angle;
    public GameObject[] Templates;
    public Vector3[] Pos;
    public Quaternion[] AngleJumpscare;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("TriggeredCheck");
        JumpscareChecker.instance.ResetAndSetTrigger(this);
        state = true;
    }
    private void OnTriggerExit(Collider other)
    {
        state = false;
    }
}
