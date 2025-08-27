using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
public class RaycastManager : MonoBehaviour
{
    public int maxDistance;
    public LayerMask layerMask;
    Camera mainCam;
    RaycastHit[] raycastHits = new RaycastHit[1];
    public GameObject Selected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHitUpdate();
    }

    void RaycastHitUpdate()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            
            switch (hit.collider.tag)
            {
                case "PC":
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        print("jink");
                        EyeViewManager.Instance.Blink(0.25f);
                    }
                    break;

            }

        }
        else
        {
            Selected = null;
        }
    }
}
