using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpscareSpawnChecker : MonoBehaviour
{
    public static PlayerJumpscareSpawnChecker current { get; private set; }

    /*
    public float jarakRaycast = 5f;

    public LayerMask layerRintangan;

    [Tooltip("Indeks: \n0=Depan, \n1=Serong Depan Kanan, \n2=Kanan, \n3=Serong Belakang Kanan, \n4=Belakang, \n5=Serong Belakang Kiri, \n6=Kiri, \n7=Serong Depan Kiri")]
    public int[] AngleRays;
    public bool[] statusArah = new bool[8];
    [SerializeField] Vector3[] Arah = new Vector3[8];
    private void Awake()
    {
        current = this;
    }
    void Update()
    {

        for (int i = 0; i < AngleRays.Length; i++)
        {

            Vector3 arah = Quaternion.AngleAxis(AngleRays[i], transform.up) * transform.forward;
            Arah[i] = arah;
            statusArah[i] = CekArah(arah);
        }
    }
    private bool CekArah(Vector3 arah)
    {

        bool menabrak = Physics.Raycast(transform.position, arah, jarakRaycast, layerRintangan);

        Debug.DrawRay(transform.position, arah * jarakRaycast, menabrak ? Color.red : Color.green);

        return !menabrak;
    }

    public Vector3 GetJumpscarePoint()
    {
        List<Vector3> CorrectPoint = new List<Vector3>();
        for(int x = 0; x < 8; x++)
        {
            if (statusArah[x] == true)
            {
                CorrectPoint.Add(Arah[x]);
            }
        }

        int point = Random.Range(0,CorrectPoint.Count);

        return CorrectPoint[point] * (jarakRaycast / 2);
    */
    public JCRay[] Rays;
    private void Awake()
    {
        current = this;
    }
    public JCRay GetJumpscarePoint()
    {
        List<JCRay> CorrectPoint = new List<JCRay>();
        for (int x = 0; x < Rays.Length; x++)
        {
            if (Rays[x].collided == false)
            {
                CorrectPoint.Add(Rays[x]);
            }
        }
        print(CorrectPoint.Count);
        if (CorrectPoint.Count == 0)
        {
            return null;
        }
        else
        {
            int point = Random.Range(0, CorrectPoint.Count - 1);

            return CorrectPoint[point];
        }
    } 



    }
