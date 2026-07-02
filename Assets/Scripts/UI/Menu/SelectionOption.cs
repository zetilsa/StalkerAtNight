using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SelectionOption : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Label;
    public int value;
    public string[] texts;
    public UnityEvent OnChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeData(int state)
    {
        value = state;
        Label.text = texts[state];
        OnChanged.Invoke();
    }
    public void ChangeData(int state, bool DontInvoke)
    {
        value = state;
        Label.text = texts[state];
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (value == texts.Length - 1)
        {
            value = 0;

        }
        else
        {
            value++;
        }
        Label.text = texts[value];
        OnChanged.Invoke();
    }
    public void Previous()
    {
        if (value == 0)
        {
            value = texts.Length - 1;

        }
        else
        {
            value--;
        }
        Label.text = texts[value];
        OnChanged.Invoke();
    }
}
