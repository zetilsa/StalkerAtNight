using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {  get; private set; }

    public bool OnComputer;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



}
