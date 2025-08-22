using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Drawer_Pull_X : MonoBehaviour
{
    public bool isOn;
    public Vector3 falseval;
    public Vector3 trueval;
    bool transition;
    public void Turn(bool value)
    {
        if(transition == false)
        {
            transition = true;
            isOn = value;
            if (isOn == true)
            {
                transform.DOLocalMove(trueval, 1).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    transition = false;
                });
            }
            else
            {
                transform.DOLocalMove(falseval, 1).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    transition = false;
                });
            }
        }

    }

    public void Turn()
    {
        if (transition == false)
        {
            transition = true;
            isOn = !isOn;
            if (isOn == true)
            {
                transform.DOLocalMove(trueval, 1).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    transition = false;
                });
            }
            else
            {
                transform.DOLocalMove(falseval, 1).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    transition = false;
                });
            }
        }
    }

}
