using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TheGhostMoveControl : MonoBehaviour
{
    public Transform moveObj;

    public void MoveTo(Vector3 to, float duration)
    {
        moveObj.DOMove(to, duration);
    }

    public void Move(Vector3 from, Vector3 to, float duration)
    {
        moveObj.position = from;
        moveObj.DOMove(to, duration);
    }

    public void TeleportTo(Vector3 to)
    {
        moveObj.position = to;
    }

    public void LookAt(Vector3 target)
    {
        float d = target.x - moveObj.position.x;
        SetDirection(d < 0);
    }
    public void SetDirection(bool isLeft)
    {
        if (isLeft)
            moveObj.localScale = moveObj.localScale.V3SetX(-1);
        else
            moveObj.localScale = moveObj.localScale.V3SetX(1);
    }

    public void StopAction()
    {
        moveObj.DOKill();
    }
}
