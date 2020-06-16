using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControl : MonoBehaviour, IMonsterControl
{
    #region Fields
    public MaskAnimControl AnimMask;
    public MaskMoveControl moveControl;
    public AttackRange attackRange;
    public float countDownTime;
    public bool isAttack = false;
    float timeTemp;
    Vector3 StartPosition;
    #endregion Fields

    void Start(){
        StartPosition = this.transform.position;
        //AnimMask.SetAnimation(MaskState.Idle, true);
        moveControl.viewRange.onEnter = false;
        {
            moveControl.MonsterLoopMove(this.transform.position.x);
            AnimMask.SetAnimation(MaskState.Idle, true);
        }
    }
    void Update(){
        Moving();
    }
    #region Moving
    public void Moving(){
        if(moveControl.viewRange.onEnter){
            moveControl.MonsterChasing();
            AnimMask.SetAnimation(MaskState.Idle,true);
        }
    }

    #endregion Moving

    #region Attack
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true && isAttack == false) {
            AnimMask.SetAnimation(MaskState.Attack,false);
            isAttack = true;
        }
            AttackCooldown();
    }
    public void AttackCooldown(){
        countDownTime -= Time.deltaTime;
        if(countDownTime <= 0){
            EndAttack();
        }
    }
    public void EndAttack(){
        isAttack = false;
        AnimMask.SetAnimation(MaskState.Idle,true);
     countDownTime = timeTemp;
    }
    #endregion Attack
    public void GetHit(){
        AnimMask.SetAnimation(MaskState.Die,false);
    }
}
