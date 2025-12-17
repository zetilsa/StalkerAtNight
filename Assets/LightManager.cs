using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField]MeshRenderer m_Renderer;
    [SerializeField]Material[] m_Material;
    [SerializeField] Light Light;
    bool On;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeStateLight()
    {
        On = !On;
        lightchange();
    }

    public void ChangeStateLight(bool value)
    {
        On = value;
        lightchange();
    }

    void lightchange()
    {
        Light.enabled = On;
        if(On == true)
        {
            m_Renderer.material = m_Material[0];
        }
        else
        {
            m_Renderer.material = m_Material[1];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
