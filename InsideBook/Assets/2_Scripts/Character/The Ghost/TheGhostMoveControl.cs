using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TheGhostMoveControl : MonsterMoveController
{
    Vector3 tempPosition = Vector3.zero;
    public void MoveToAttack1(Transform Top, Transform GroundLevel, Transform target, float waitTime)
    {
        tempPosition = Top.position;
        tempPosition.x = GroundLevel.position.x;
        TeleportTo(tempPosition);
        MoveTo(GroundLevel, waitTime);
        LookAt(target);
    }

    public void MoveToAttack3(Transform Top, Transform GroundLevel, float waitTime)
    {
        tempPosition.y = Top.position.y;
        tempPosition.x = GroundLevel.position.x;
        Move(tempPosition, GroundLevel.position, waitTime);
    }

    public void DashAttackByHeight(Vector3 from, Vector3 to, Vector3 height, float atk_spd)
    {
        tempPosition.y = height.y;

        tempPosition.x = from.x;
        TeleportTo(tempPosition);
        tempPosition.x = to.x;
        MoveTo(tempPosition, atk_spd);
    }
    public void DashAttackByHeight(Transform from, Transform to, Transform height, float atk_spd)
    {
        DashAttackByHeight(from.position, to.position, height.position, atk_spd);
    }

    public void DashAttack(Vector3 from, Vector3 to, float atk_spd)
    {
        TeleportTo(from);
        MoveTo(to, atk_spd);
    }
    public void DashAttack(Transform from, Transform to, float atk_spd)
    {
        DashAttack(from.position, to.position, atk_spd);
    }
}
