using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TheGhostControl : MonoBehaviour
{
    public Transform target;
    public TheGhostMoveControl moveController;
    public TheGhostAnimControl AnimTheGhost;

    [Header("Attack Position")]
    public Transform Origin;
    public Transform Top;
    public Transform Bottom;
    public Transform Left;
    public Transform Right;

    Coroutine AttactRoutine;

    float raiseTime = 1.833f;
    public void Raise()
    {
        AnimTheGhost.SetAnimation(TheGhostState.Raise, false);
        DOVirtual.DelayedCall(raiseTime, () =>
        {
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            DOVirtual.DelayedCall(time_step1, () =>
            {
                AttactRoutine = StartCoroutine(AttackRoute());
            });
        });
    }

    float time_step1 = 2;
    float time_step2 = 1.667f;
    float time_step3 = 3;
    float time_step4 = 3; //1.333f;
    IEnumerator AttackRoute()
    {

        float atkSpeed = 0;
        while (true)
        {
            AnimTheGhost.SetAnimation(TheGhostState.Idle2);
            atkSpeed = 0.5f;
            tempPosition = Top.position;
            tempPosition.x = this.transform.position.x;
            moveController.MoveTo(tempPosition, atkSpeed);
            moveController.LookAt(target.position);
            yield return new WaitForSeconds(time_step1 + atkSpeed);
            //=== Attack 1
            //=== Attack 2
            atkSpeed = 1.667f;
            //=== Attack 1
            //=== Attack 2
            atkSpeed = 1.667f;
            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            DashAttack(Right.position, Left.position, atkSpeed);
            moveController.LookAt(target.position);
            yield return new WaitForSeconds(time_step2);
            //=== Attack 2
            AnimTheGhost.SetAnimation(TheGhostState.Attack2);
            DashAttack(Left.position, Right.position, atkSpeed);
            moveController.LookAt(target.position);
            yield return new WaitForSeconds(time_step2);
            //=== Attack 3
            atkSpeed = 1.333f;
            moveController.Move(Top.position, Origin.position, atkSpeed);
            AnimTheGhost.SetAnimation(TheGhostState.Power, false);
            moveController.LookAt(target.position);
            yield return new WaitForSeconds(atkSpeed);
            AnimTheGhost.SetAnimation(TheGhostState.Attack3, false);
            yield return new WaitForSeconds(time_step3);
            //=== Tired
            AnimTheGhost.SetAnimation(TheGhostState.Tired);
            yield return new WaitForSeconds(time_step4);
            moveController.LookAt(target.position);
        }
    }

    Vector3 tempPosition = Vector3.zero;
    public void DashAttack(Vector3 from, Vector3 to, float atk_spd)
    {
        tempPosition.y = target.position.y;

        tempPosition.x = from.x;
        moveController.TeleportTo(tempPosition);
        tempPosition.x = to.x;
        moveController.MoveTo(tempPosition, atk_spd);

        AnimTheGhost.DashFX.SetActive(true);
        AnimTheGhost.SlashFX.SetActive(true);
    }
}
