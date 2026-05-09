using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class Originalitas : MonoBehaviour
{
    [SerializeField]bool[] state;
    [SerializeField] string[] keycode;
    bool isOn;
    [SerializeField] int x;

    [SerializeField] CanvasGroup cg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        state = new bool[keycode.Length];
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn == false)
        {

                if(x == 0)
                {

                    if (Input.GetKeyDown(keycode[x]))
                    {
                        state[x] = true;
                    x++;
                    }
                    
                }
                else if (x != 0)
                {

                    if (Input.GetKeyDown(keycode[x]) && state[x - 1] == true)
                    {
                        state[x] = true;
                    x++;
                    }
                }

            
            if (state[state.Length - 1] == true)
            {
                for (int y = 0; y < state.Length; y++)
                {
                    state[y] = false;
                }
                x = 0;

                isOn = true;
                cg.DOFade(1, 1).OnComplete(() =>
                {
                    cg.blocksRaycasts = true;
                });
            }
        }
    }
    

    public void Close()
    {
        cg.blocksRaycasts = false;
        cg.DOFade(0, 1).OnComplete(() =>
        {
            isOn = false;
        });
    }
}
