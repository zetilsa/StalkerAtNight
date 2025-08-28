using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public FirstPersonController MainFPS;
    public Transform CameraJoint;
    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
