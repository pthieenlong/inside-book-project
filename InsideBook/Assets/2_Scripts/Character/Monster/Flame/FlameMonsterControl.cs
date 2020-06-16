using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMonsterControl : MonoBehaviour
{
    #region Fields
    public FlameMonsterAnimControl AnimFlame;
    public FlameMonsterMoveControl moveControl;
    public AttackRange attackRange;
    public float countDownTime;
    public bool isAttack = false;
    float timeTemp;
    Vector3 StartPosition;
    #endregion Fields

    void Start(){
        StartPosition = this.transform.position;
        AnimFlame.SetAnimation(FlameState.Idle, true);
        
    }
    void Update(){
    }
    #region Moving
    

    #endregion Moving

    #region Attack
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true && isAttack == false) {
            AnimFlame.SetAnimation(FlameState.Attack,false);
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
        AnimFlame.SetAnimation(FlameState.Idle,true);
     countDownTime = timeTemp;
    }
    #endregion Attack
}


