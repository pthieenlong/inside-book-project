using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMonster : SpineAnimControl
{
    public MonsterSystem monsterSystem;
    void Start(){
        SetAnimation(BugState.Idle,true);
    }
    void Update(){
        monsterSystem.LookAtPlayer();
        if(monsterSystem.isLoopMove){
            monsterSystem.MonsterLoopMove();
        }
        monsterSystem.MonsterAttack();
        if(monsterSystem.isAttack == true){
            SetAnimation(BugState.Attack);
        }
        if(monsterSystem.isAttack == false && monsterSystem.viewRange.isFollow){
            SetAnimation(BugState.Move);
        } else 
            SetAnimation(BugState.Idle);
    }
    void LateUpdate(){
        if(monsterSystem.viewRange.isFollow == true){
            SetAnimation(BugState.Move, true);
            monsterSystem.MonsterChasing();

        }
        if(GamePlaySetting.IsDead == true){
            SetAnimation(BugState.Die, false);
            monsterSystem.viewRange.isFollow = false;

        } 
    }
}
public class BugState
{
    public const string Move = "di chuyen";
    public const string Idle = "tho";
    public const string Die = "hit";
    public const string Attack = "skill";
}
