using UnityEngine;

public class HarrisCheck : MonoBehaviour
{
    [SerializeField] JumpscareChecker JC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.IsHiding == true)
        {
            JC.gameObject.SetActive(false);
        }
        else
        {
            JC.gameObject.SetActive(true);
        }
    }
}
