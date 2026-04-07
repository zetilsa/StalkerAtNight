using TMPro;
using UnityEngine;

public class RandomTip : MonoBehaviour
{
    [SerializeField] string[] tips;
    [SerializeField] TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = $"Tip : {tips[Random.Range(0, tips.Length - 1)]}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
