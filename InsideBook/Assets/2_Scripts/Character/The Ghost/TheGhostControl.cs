using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TheGhostControl : MonsterController
{
    public TheGhostMoveControl MoveController;
    public TheGhostAnimControl AnimControl;
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

    Coroutine AttackRoutine;
    Vector3 tempPosition = Vector3.zero;

    public void Init()
    {
        ResetTween();
        AnimControl.SetAnimation(TheGhostState.Idle);
        MoveController.SetDirection(true);
        MoveController.TeleportTo(GroundLevel_Mid);
        ResetCollider();
    }
    public void ResetTween()
    {
        this.DOKill(true);
        MoveController.DOKill(true);
        MoveController.moveObj.DOKill(true);
        this.StopAllCoroutines();
    }
    public void ResetCollider()
    {
        C_Body.SetActive(false);
        C_Weakpoint.SetActive(false);
        C_Atk1.SetActive(false);
        C_Atk2.SetActive(false);
        C_Atk3.SetActive(false);
    }
    public void SetActiveWeakPoint(bool isActive)
    {
        C_Body.SetActive(!isActive);
        C_Weakpoint.SetActive(isActive);
    }

    //========= Action =========
    float raiseTime = 1.833f;
    public void Raise()
    {
        AnimControl.SetAnimation(TheGhostState.Raise, false);
        DOVirtual.DelayedCall(raiseTime, () =>
        {
            AnimControl.SetAnimation(TheGhostState.Idle2);
            DOVirtual.DelayedCall(prepareTime, () =>
            {
                AttackRoutine = StartCoroutine(AttackRoute());
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
            AnimControl.SetAnimation(TheGhostState.Idle2);
            waitTime = 0.5f;
            tempPosition = Top.position;
            tempPosition.x = this.transform.position.x;
            MoveController.MoveTo(tempPosition, waitTime);
            MoveController.LookAt(target);
            yield return new WaitForSeconds(prepareTime + waitTime);
            //=== Attack 1
            AnimControl.SetAnimation(TheGhostState.Idle2);
            MoveController.MoveToAttack1(Top, GroundLevel_Left, target, waitTime);
            SetActiveWeakPoint(false);
            yield return new WaitForSeconds(waitTime);

            AnimControl.SetAnimation(TheGhostState.Attack1, false);
            C_Atk1.SetActive(true);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimControl.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            MoveController.DashAttackByHeight(GroundLevel_Left, Right, GroundLevel_Mid, atk_speed2);
            MoveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 1
            AnimControl.SetAnimation(TheGhostState.Idle2);
            MoveController.MoveToAttack1(Top, GroundLevel_Right, target, waitTime);
            yield return new WaitForSeconds(waitTime);

            AnimControl.SetAnimation(TheGhostState.Attack1, false);
            C_Atk1.SetActive(true);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimControl.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            MoveController.DashAttackByHeight(GroundLevel_Right, Left, GroundLevel_Mid, atk_speed2);
            MoveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimControl.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            tempPosition.x = Left.position.x;
            tempPosition.y = GroundLevel_Mid.position.y;
            MoveController.DashAttack(Right.position, tempPosition, atk_speed2);
            MoveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 2
            AnimControl.SetAnimation(TheGhostState.Attack2);
            C_Atk2.SetActive(true);
            tempPosition.x = Right.position.x;
            tempPosition.y = GroundLevel_Mid.position.y;
            MoveController.DashAttack(Left.position, tempPosition, atk_speed2);
            MoveController.LookAt(target);
            yield return new WaitForSeconds(atk_speed2);

            //=== Attack 3
            AnimControl.SetAnimation(TheGhostState.Power, false);
            MoveController.TeleportTo(Top);
            MoveController.LookAt(target);
            if (MoveController.moveObj.localScale.x < 0)
                MoveController.MoveToAttack3(Top, GroundLevel_Right, waitTime);
            else
                MoveController.MoveToAttack3(Top, GroundLevel_Left, waitTime);
            yield return new WaitForSeconds(waitTime);

            AnimControl.SetAnimation(TheGhostState.Attack3, false);
            C_Atk3.SetActive(true);
            yield return new WaitForSeconds(atk_speed3);

            //=== Tired
            AnimControl.SetAnimation(TheGhostState.Tired);
            SetActiveWeakPoint(true);

            yield return new WaitForSeconds(tired_time);
            MoveController.LookAt(target);
        }
    }

    IEnumerator GetHitRoute()
    {
        AnimControl.SetAnimation(TheGhostState.GetHit, false);
        ResetCollider();
        yield return new WaitForSeconds(1.333f);

        if (HP > 0)
        {
            AnimControl.SetAnimation(TheGhostState.Idle2);
            AttackRoutine = StartCoroutine(AttackRoute());
        }
        else
        {
            StartCoroutine(DeadRoute());
        }
        yield break;

    }

    IEnumerator DeadRoute()
    {
        AnimControl.SetAnimation(TheGhostState.Tired);
        MoveController.MoveTo(GroundLevel_Mid, 3);
        yield return new WaitForSeconds(3);

        AnimControl.SetAnimation(TheGhostState.Die, false);
        yield return new WaitForSeconds(3.5f);
    }

    public override void OnDead()
    {
        base.OnDead();
        if (AttackRoutine != null)
        {
            StopCoroutine(AttackRoutine);
        }
    }

    #region Interface Monster Control
    public override void GetHit(int dmg)
    {
        base.GetHit(dmg);
        if (AttackRoutine != null)
        {
            StopCoroutine(AttackRoutine);
        }
        StartCoroutine(GetHitRoute());
    }
    #endregion Interface Monster Control
}
