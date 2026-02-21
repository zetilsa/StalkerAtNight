using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AIManager : MonoBehaviour
{
    public static AIManager instance {  get; private set; }

    [SerializeField]AIEnemy[] Enemies;
    public float GenerateInterval;
    [SerializeField] float randomizedNumber;
    [SerializeField] Vector2 GlitchTimeRange;
    public float GlitchTime;
    public Room[] Rooms;
    [SerializeField] PlayableDirector EntranceAnim;
    [SerializeField] TimelineAsset[] EntranceSet;


    [SerializeField] bool AutoStartTick;
    // opo iki cah
    void Awake()
    {
        instance = this;
        if (AutoStartTick)
        {
            StartTick();
        }
    }

    // riffat stupid ass
    void Update()
    {

    }
    public void DoEntrance(int ID)
    {
        EntranceAnim.playableAsset = EntranceSet[ID];
        EntranceAnim.Play();

    }
    public void DoneEntranceAnim()
    {

        StartTick(true);
    }
    public void SetAlwaysOn(Room room)
    {
        room.AlwaysOn = true;
        room.gameObject.SetActive(true);
    }
    public void UnSetAlwaysOn(Room room)
    {
        room.AlwaysOn = false;
        
    }
    public void StartTick()
    {
        InvokeRepeating("GenerateNumber", GenerateInterval, GenerateInterval);
    }
    public void StartTick(bool reboot)
    {
        InvokeRepeating("GenerateNumber", GenerateInterval, GenerateInterval);
        if(reboot == true)
        {
            foreach (AIEnemy enemy in Enemies)
            {
                if (enemy.currentposition == 8)
                {
                    enemy.Move();
                }
            }
            }
    }
    public void StopTick()
    {
        CancelInvoke();
    }

    void GenerateNumber()
    {
        randomizedNumber = Random.Range(0, 20);
        print("RandomNumber : " + randomizedNumber);

        foreach (AIEnemy enemy in Enemies)
        {
            print("Checking..");
            if(enemy.CheckCurrentNumber(randomizedNumber) == true)
            {
                print("it right now calling enemy to move");
                GlitchTime = Random.Range(GlitchTimeRange.x,GlitchTimeRange.y);
                if (enemy.currentposition != -1)
                {
                    CCTVManager.instance.buttons[enemy.currentposition].CalculateGlitch(GlitchTime);
                }
                enemy.Move();
            }
        }
    }

    public void SetGlitchOnCameras(int target)
    {

        CCTVManager.instance.buttons[target].CalculateGlitch(GlitchTime);
    }
}
