using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class JumpscareChecker : MonoBehaviour
{
    public static JumpscareChecker instance { get; private set; }
    JumpscareCheckTrigger current;
    Transform FPS;
    Transform CAM;
    [SerializeField]int CalculatedAngleState;
    [SerializeField] float FPSY;
    bool check = true;
    public bool JumpscareEnabled;
    bool JumpscareSet;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        FPS = GameManager.instance.MainFPS.transform;
        CAM = GameManager.instance.CameraJoint.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check();
        if (JumpscareEnabled == true)
        {
            if(JumpscareSet == true)
            {
                JumpscareEnabled = false;
                JumpscareSet = false;
                Jumpscare();
            }
        }
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(0);
        FPSY = FPS.eulerAngles.y;
        if (check == true)
        {
            if (current != null)
            {
                bool loop = false;
                //mencari posisi dengan kalkulasi posisi dan angle player
                for (int i = 0; i < current.Angle.Length; i++)
                {
                    print("Kalkulasi ke-" + i);
                    if (FPS.eulerAngles.y > current.Angle[i].x && FPS.rotation.y < current.Angle[i].y)
                    {
                        loop = true;
                        print("Kalkulasi jumpscare benar ke-" + i);
                        CalculatedAngleState = i;
                    }
                    else
                    {
                        print("Kalkulasi jumpscare salah ke-" + i);
                    }
                }
                if(loop == false)
                {
                    print("Kalkulasi jumpscare gak ada yg bener, angle pake index paling akhir");
                    CalculatedAngleState = current.Angle.Length - 1;
                }
            }

        }
        StartCoroutine(Check());
    }
    public void EnableJumpscare(bool enable)
    {
        JumpscareEnabled = enable;
    }
    public void Jumpscare()
    {
        GameManager.instance.MainFPS.playerCamera.GetComponent<CinemachineBrain>().DefaultBlend.Time = .5f;
        Check();
        check = false;
        
        PlayerManager.instance.ChangeControlState(false, false, false, false, false, true, false, true, false);
        Transform JumpscareInstance = Instantiate(current.Templates[CalculatedAngleState], current.Pos[CalculatedAngleState], current.AngleJumpscare[CalculatedAngleState]).transform;
        FPS.GetComponent<FirstPersonController>().enabled = false;
        CAM.DORotateQuaternion(JumpscareInstance.GetComponent<JumpscareTemplateMetaData>().CameraPoint.rotation, .3f).SetEase(Ease.OutCubic);
        CAM.DOMove(JumpscareInstance.GetComponent<JumpscareTemplateMetaData>().CameraPoint.position, 0.3f);
    }
    public void ResetAndSetTrigger(JumpscareCheckTrigger trigger)
    {
        current = trigger;
        JumpscareSet = true;
    }

}
