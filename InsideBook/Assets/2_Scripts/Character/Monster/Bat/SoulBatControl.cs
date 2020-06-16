using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBatControl : MonoBehaviour, IMonsterControl
{
    #region Fields
    public SoulBatAnimControl AnimSoul;
    public SoulBatMoveControl moveControl;
    public AttackRange attackRange;
    public float countDownTime;
    public bool isAttack = false;
    float timeTemp;
    Vector3 StartPosition;
    #endregion Fields

    void Start(){
        StartPosition = this.transform.position;
        AnimSoul.SetAnimation(SoulBatState.Idle, true);
        
        
    }
    void Update(){
        Moving();
    }
    #region Moving
    public void Moving(){
        if(moveControl.viewRange.onEnter){
            moveControl.MonsterChasing();
            AnimSoul.SetAnimation(SoulBatState.Idle,true);
        }
    }

    #endregion Moving

    #region Attack
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true && isAttack == false) {
            AnimSoul.SetAnimation(SoulBatState.Attack,false);
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
        AnimSoul.SetAnimation(SoulBatState.Idle,true);
     countDownTime = timeTemp;
    }
    #endregion Attack
    public void GetHit(){
        AnimSoul.SetAnimation(SoulBatState.Die,false);
    }
}


