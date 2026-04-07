using Unity.Cinemachine;
using UnityEngine;

public class MaxFrameRateFinder : MonoBehaviour
{
    [SerializeField] SelectionOption MaxFrameRateOption;
    [SerializeField] Vector2 range;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for(int x = (int)range.x; x < range.y; x += 5)
        {
            MaxFrameRateOption.texts[x] = x.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
