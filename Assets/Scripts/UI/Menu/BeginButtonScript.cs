using UnityEngine;

public class BeginButtonScript : MonoBehaviour
{
    [SerializeField]DataSaveLoader DSL;
    [SerializeField] MenuUITransitionHandler MTH;
    [SerializeField] CanvasGroup NightSelection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Calculate()
    {
        DSL = DataSaveLoader.instance;

        if(DSL.GetData<int>("LastNight") == 0)
        {
            GameSystem.instance.NewGame();
        }
        else
        {
            MTH.MulaiTransisiKece(NightSelection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
