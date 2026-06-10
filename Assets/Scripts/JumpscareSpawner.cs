using DG.Tweening;
using UnityEngine;

public class JumpscareSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Templates;
    public bool check { get; set; }
    public int currentEnemyType { get; set; }
    bool jumpscaring;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (check == false) return;
        if (jumpscaring == true) return;
        switch (currentEnemyType)
        {
            case 0: //Harris
                if (PlayerManager.instance.IsHiding == false)
                {
                    jumpscaring = true;
                    Jumpscare();
                }
                break;
            case 1:
                if (PlayerManager.instance.IsHiding == false || PlayerManager.instance.IsHiding == true && PlayerManager.instance.recoverbreath == true)
                {
                    jumpscaring = true;
                    Jumpscare();
                }
                break;

            case 2:

                break;
            case 3:

                break;

        }
        
    }

    public void Check(int EnemyType)
    {
        
    }
    public void Jumpscare()
    {
        GameManager.instance.SetCameraBlendValue(.3f);
        JCRay SpawnPoint = PlayerJumpscareSpawnChecker.current.GetJumpscarePoint();
        InGamePauseSystem.instance.EnablePausing = false;
        Vector3 Jumpscarepoint = SpawnPoint.pos.position;
        Transform JumpscareInstance = Instantiate(Templates[0],new Vector3(Jumpscarepoint.x, 0.512f, Jumpscarepoint.z),SpawnPoint.pos.rotation).transform;
        
       
        

        PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, false, true, false);
        
        GameManager.instance.MainFPS.enabled = false;
        
    }
}
