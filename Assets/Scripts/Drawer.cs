using UnityEngine;
using DG.Tweening;
using System;
using Unity.Mathematics;
public class Drawer : MonoBehaviour
{
    [SerializeField] float Min;
    [SerializeField] float Max;
    [SerializeField] float time;
    bool state;
    float movevalue;
    bool interacted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    /*public void Interact()    Deprecataed
    {
        state = !state;

        if(state == false)
        {
            transform.DOLocalMoveX(Min, time).SetEase(Ease.InOutCubic);
        }
    else if (state == true)
        {
            transform.DOLocalMoveX(Max, time).SetEase(Ease.InOutCubic);
        }
    }*/

    public void Interact(bool value)
    {
        interacted = value;

    }
    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            Vector3 worldMoveDir = transform.right;
            Vector3 screenPoint1 = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 screenPoint2 = Camera.main.WorldToScreenPoint(transform.position + worldMoveDir);
            Vector2 screenMoveDir = (Vector2)(screenPoint2 - screenPoint1).normalized;
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            float finalInput = Vector2.Dot(mouseInput, screenMoveDir) * 0.1f;
            float newX = Mathf.Clamp(transform.localPosition.x + finalInput, Min, Max);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);

            if (Input.GetMouseButtonUp(0))
            {
                Interact(false);
                GameManager.instance.MainFPS.cameraCanMove = true;
            }
        }
    }
}
