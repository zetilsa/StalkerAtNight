using UnityEngine;

public class RoomEnemyVariant : MonoBehaviour
{
    public GameObject[] Variasi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableEnemyInThisRoom()
    {
        foreach(GameObject Enemy in Variasi)
        {
            Enemy.SetActive(false);
        }
    }

    public void ShowEnemyInThisRoom()
    {
        int RandomVariant = Random.Range(0,Variasi.Length - 1);
        Variasi[RandomVariant].SetActive(true);
    }
}
