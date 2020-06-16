using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMonsterControl : MonoBehaviour, IMonsterControl
{
    #region Fields
    public BugMonsterAnimControl AnimBug;
    public BugMonsterMoveControl moveControl;
    public AttackRange attackRange;
    public float countDownTime;
    public bool isAttack = false;
    float timeTemp;
    Vector3 StartPosition;
    #endregion Fields

    void Start(){
        StartPosition = this.transform.position;
        //AnimBug.SetAnimation(BugState.Idle, true);
        moveControl.viewRange.onEnter = false;
        {
            moveControl.MonsterLoopMove(this.transform.position.x);
            AnimBug.SetAnimation(BugState.Move, true);
        }
    }
    void Update(){
        Moving();
    }
    #region Moving
    public void Moving(){
        if(moveControl.viewRange.onEnter){
            moveControl.MonsterChasing();
            AnimBug.SetAnimation(BugState.Move,true);
        }
    }

    #endregion Moving

    #region Attack
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true && isAttack == false) {
            AnimBug.SetAnimation(BugState.Attack,false);
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
        AnimBug.SetAnimation(BugState.Idle,true);
     countDownTime = timeTemp;
    }
    #endregion Attack
    public void GetHit(){
        AnimBug.SetAnimation(BugState.Die);
    }
    // public MonsterSystem monsterSystem;
    // float startPos;
    // void Start(){
    //     startPos = this.transform.position.x;

    //     SetAnimation(BugState.Idle,true);
    // }
    // void Update(){
    //     monsterSystem.LookAtPlayer();
    //     if(monsterSystem.isLoopMove){
    //         monsterSystem.MonsterLoopMove(startPos);
    //     }
    //     monsterSystem.MonsterAttack();
    //     if(monsterSystem.isAttack == true){
    //         SetAnimation(BugState.Attack);
    //     }
    //     if(monsterSystem.isAttack == false && monsterSystem.viewRange.onEnter){
    //         SetAnimation(BugState.Move);
    //     } else 
    //         SetAnimation(BugState.Idle);
    // }
    // void LateUpdate(){
    //     if(monsterSystem.viewRange.onEnter == true){
    //         SetAnimation(BugState.Move, true);
    //         monsterSystem.MonsterChasing();

    //     }
    //     if(GamePlaySetting.IsDead == true){
    //         SetAnimation(BugState.Die, false);
    //         monsterSystem.viewRange.onEnter = false;

    //     } 
    // }
   
}

