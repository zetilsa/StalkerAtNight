using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;
public class SleepMechanic : MonoBehaviour
{
    [SerializeField] CinemachinePanTilt CPT;

    [SerializeField]float tiltValue;
    GameManager gmanager;
    PlayerManager playerManager;
    [SerializeField] Vector2 sleepTiltRange;
    bool getdata;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        gmanager = GameManager.instance;
        playerManager = PlayerManager.instance;

        

    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExitBed());
        }
    }

    IEnumerator ExitBed()
    {
        if (RaycastManager.Instance.OnCameraTransition == false)
        {
            RaycastManager.Instance.OnCameraTransition = true;
            EyeViewManager.Instance.Blink(0.25f);
            yield return new WaitForSeconds(0.25f);

            if (playerManager.OnBed == true)
            {
                playerManager.OnBed = false;
                GuideTextManager.instance.Hide();
                CrosshairManager.instance.SetShow(true);
                playerManager.BedCamera.SetActive(false);
                playerManager.ChangeControlState(true, true, true, true, true, true, false, true, false);
                playerManager.DoSomething("UnSleep");
                RaycastManager.Instance.OnCameraTransition = false;
            }
        }
    }
}
