using UnityEngine;

public class HarrisCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.enableHoldBreath == true && PlayerManager.instance.recoverbreath == false)
        {
            
        }
        else
        {
            GameManager.instance.GameOver(); 
        }
    }
}
