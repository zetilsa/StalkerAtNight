using UnityEngine;
using DG.Tweening;
public class Pintu : MonoBehaviour
{
    public bool statepintu; //terbuka = true, tertutup = false
    public Vector3 RotasiPintuTerbuka;
    public Vector3 RotasiPintuTertutup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteraksiPintu()
    {
        statepintu = !statepintu;  //mengubah state menjadi sebaliknya, misal true menjadi false, false menjadi true

        if (statepintu == true)
        {
            //terbuka

            transform.DOLocalRotate(RotasiPintuTerbuka, 1); //Argumennya (target rotasi Vector3, durasi transisi float)
        }
        else if (statepintu == false)
        {
            //tertutup

            transform.DOLocalRotate(RotasiPintuTertutup, 1);
        }
    }
}
