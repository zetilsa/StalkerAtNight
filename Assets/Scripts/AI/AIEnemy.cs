using System.Collections;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    AIManager AIM;//bad aim mb wkwkw

    public float DifficultyLevel = 3;
    public int currentposition = -1; //kalo -1 artinya belum masuk yaaaa :P <3
    //titit

    [SerializeField] int EnemyID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AIM = AIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckCurrentNumber(float value)
    {
        if(value <= DifficultyLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Move()
    {
        IEnumerator w()
        {
            //cari tetangga mana aja
            if (currentposition != -1)
            {
                int TetanggaLength = AIM.Rooms[currentposition].tetangga.Length;


                //kalkulasi mau pindah kemana
                int NextRoom = AIM.Rooms[currentposition].tetangga[Random.Range(0, TetanggaLength)].RoomID;

                if (AIM.Rooms[currentposition].isTheLastRoom == false && currentposition != 8)
                {
                    if (NextRoom != 8)
                    {
                        AIManager.instance.SetGlitchOnCameras(NextRoom);

                    }
                    yield return new WaitForSeconds(AIManager.instance.GlitchTime / 2);
                    AIManager.instance.Rooms[currentposition].EnemiesinRoom.Enemies[EnemyID].DisableEnemyInThisRoom();
                }
                print("Moved : Harris from" + currentposition + " to " + NextRoom);
                currentposition = NextRoom;
                if (currentposition != 8)
                {
                    yield return new WaitForSeconds(AIManager.instance.GlitchTime / 2);
                    AIManager.instance.Rooms[currentposition].EnemiesinRoom.Enemies[EnemyID].ShowEnemyInThisRoom();
                }
                else
                {
                    AIM.StopTick();
                    IEnumerator i()
                    {
                        print("enemy entered ROom, jumpscaring");
                        yield return new WaitForSeconds(2);
                        AIManager.instance.DoEntrance(EnemyID);
                    }
                    StartCoroutine(i());
                }
            }


            else
            {

                int NextRoom = 0;
                AIManager.instance.SetGlitchOnCameras(NextRoom);
                print("Moved : Harris from" + currentposition + " to " + NextRoom);
                currentposition = NextRoom;
                AIManager.instance.Rooms[currentposition].EnemiesinRoom.Enemies[EnemyID].ShowEnemyInThisRoom();
            }
        }
        StartCoroutine(w());
    }
}
