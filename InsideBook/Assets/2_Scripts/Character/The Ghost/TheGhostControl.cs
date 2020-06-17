using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TheGhostControl : MonoBehaviour
{
    public Transform target;
    public TheGhostMoveControl moveController;
    public TheGhostAnimControl AnimTheGhost;
    [Header("Colliders")]
    public GameObject C_Body;
    public GameObject C_Weakpoint;
    public GameObject C_Atk1;
    public GameObject C_Atk2;
    public GameObject C_Atk3;

    [Header("Attack Position")]
    public Transform GroundLevel_Mid;
    public Transform GroundLevel_Left;
    public Transform GroundLevel_Right;
    public Transform Top;
    public Transform Bottom;
    public Transform Left;
    public Transform Right;

    Coroutine AttactRoutine;

    public void Init()
    {
        this.DOKill(true);
        moveController.DOKill(true);
        if (AttactRoutine != null)
            StopCoroutine(AttactRoutine);

        AnimTheGhost.SetAnimation(TheGhostState.Idle);
        moveController.SetDirection(true);
        moveController.TeleportTo(GroundLevel_Mid);
        SetActiveWeakPoint(false);
        C_Atk1.SetActive(false);
        C_Atk2.SetActive(false);
        C_Atk3.SetActive(false);
    }

    float raiseTime = 1.833f;
    public void Raise()
    {
        AnimTheGhost.SetAnimation(TheGhostState.Raise, false);
        DOVirtual.DelayedCall(raiseTime, () =>
        {
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            DOVirtual.DelayedCall(prepareTime, () =>
            {
                AttactRoutine = StartCoroutine(AttackRoute());
            });
        });
    }

    float prepareTime = 1;
    float atk_speed2 = 1.667f;
    float atk_speed3 = 3;
    float tired_time = 3; //1.333f;
    float waitTime = 0;
    IEnumerator AttackRoute()
    {
        while (true)
        {
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            waitTime = 0.5f;
            tempPosition = Top.position;
            tempPosition.x = this.transform.position.x;
            moveController.MoveTo(tempPosition, waitTime);
            moveController.LookAt(target);
            yield return new WaitForSeconds(prepareTime + waitTime);
            //=== Attack 1
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            tempPosition = Top.position;
            tempPosition.x = GroundLevel_Left.position.x;
            moveController.TeleportTo(tempPosition);
            moveController.MoveTo(GroundLevel_Left, waitTime);
            moveController.LookAt(target);
            yield return new WaitForSeconds(waitTime);

            AnimTheGhost.SetAnimation(TheGhostState.Attack1, false);
            C_Atk1.SetActive(true);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            DashAttackByHeight(GroundLevel_Left, Right, GroundLevel_Mid, atk_speed2);
            moveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 1
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            tempPosition = Top.position;
            tempPosition.x = GroundLevel_Right.position.x;
            moveController.TeleportTo(tempPosition);
            moveController.MoveTo(GroundLevel_Right, waitTime);
            moveController.LookAt(target);
            yield return new WaitForSeconds(waitTime);

            AnimTheGhost.SetAnimation(TheGhostState.Attack1, false);
            C_Atk1.SetActive(true);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            DashAttackByHeight(GroundLevel_Right, Left, GroundLevel_Mid, atk_speed2);
            moveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            tempPosition.x = Left.position.x;
            tempPosition.y = GroundLevel_Mid.position.y;
            DashAttack(Right.position, tempPosition, atk_speed2);
            moveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            tempPosition.x = Right.position.x;
            tempPosition.y = GroundLevel_Mid.position.y;
            DashAttack(Left.position, tempPosition, atk_speed2);
            moveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 3
            AnimTheGhost.SetAnimation(TheGhostState.Power, false);
            moveController.LookAt(target);

            tempPosition.y = Top.position.y;
            if (moveController.moveObj.localScale.x < 0)
            {
                tempPosition.x = GroundLevel_Right.position.x;
                moveController.Move(tempPosition, GroundLevel_Right.position, waitTime);
            }
            else
            {
                tempPosition.x = GroundLevel_Left.position.x;
                moveController.Move(tempPosition, GroundLevel_Left.position, waitTime);
            }
            yield return new WaitForSeconds(waitTime);

            AnimTheGhost.SetAnimation(TheGhostState.Attack3, false);
            C_Atk3.SetActive(true);
            yield return new WaitForSeconds(atk_speed3);

            //=== Tired
            AnimTheGhost.SetAnimation(TheGhostState.Tired);
            SetActiveWeakPoint(true);

            yield return new WaitForSeconds(tired_time);
            SetActiveWeakPoint(false);
            moveController.LookAt(target);
        }
    }

    public void SetActiveWeakPoint(bool isActive)
    {
        C_Body.SetActive(!isActive);
        C_Weakpoint.SetActive(isActive);
    }

    Vector3 tempPosition = Vector3.zero;
    public void DashAttackByHeight(Vector3 from, Vector3 to, Vector3 height, float atk_spd)
    {
        tempPosition.y = height.y;

        tempPosition.x = from.x;
        moveController.TeleportTo(tempPosition);
        tempPosition.x = to.x;
        moveController.MoveTo(tempPosition, atk_spd);
    }
    public void DashAttackByHeight(Transform from, Transform to, Transform height, float atk_spd)
    {
        DashAttackByHeight(from.position, to.position, height.position, atk_spd);
    }

    public void DashAttack(Vector3 from, Vector3 to, float atk_spd)
    {
        moveController.TeleportTo(from);
        moveController.MoveTo(to, atk_spd);
    }
    public void DashAttack(Transform from, Transform to, float atk_spd)
    {
        DashAttack(from.position, to.position, atk_spd);
    }
}
