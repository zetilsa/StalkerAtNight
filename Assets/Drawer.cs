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
        if(interacted == true)
        {
            movevalue = Mathf.Clamp(Input.GetAxis("Mouse X") * .1f,-0.1f, 0.1f);

            Vector3 newPos = transform.localPosition + new Vector3(Mathf.Clamp(movevalue, -Max, Max), 0, 0);
            Vector3 finalnewpos = new Vector3(Mathf.Clamp(newPos.x, Min, Max), Mathf.Clamp(newPos.y, Min, Max), Mathf.Clamp(newPos.z, Min, Max));
            print(movevalue + " " + newPos);
            transform.localPosition = finalnewpos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Interact(false);
            GameManager.instance.MainFPS.cameraCanMove = true;
        }
    }
}
