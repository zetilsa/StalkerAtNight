using UnityEngine;

public class TuningSection : MonoBehaviour
{
    [SerializeField] SelectionOption[] Options;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply()
    {
        foreach (SelectionOption option in Options)
        {
            DataSaveLoader.instance.SetData(option.gameObject.name, option.value);
        }
    }
}
