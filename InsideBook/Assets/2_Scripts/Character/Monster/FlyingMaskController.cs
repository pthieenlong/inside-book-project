using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMaskController : SpineAnimControl
{
    public MonsterSystem monsterSystem;
    void Start()
    {
        SetAnimation(FlyingMaskState.Idle, true);
    }
    void Update(){
        monsterSystem.LookAtPlayer();
        if(monsterSystem.isLoopMove){
            monsterSystem.MonsterLoopMove();
        }
        monsterSystem.MonsterAttack();
        if(monsterSystem.isAttack == true){
            SetAnimation(FlyingMaskState.Attack);
        }
        if(monsterSystem.isAttack == false){
            SetAnimation(FlyingMaskState.Idle);
        }
    }
    void LateUpdate(){
        if(monsterSystem.viewRange.isFollow == true){
            monsterSystem.MonsterChasing();

        }
        if(GamePlaySetting.IsDead == true){
            SetAnimation(FlyingMaskState.Die, false);
            monsterSystem.viewRange.isFollow = false;

        } 
    }
}
public class FlyingMaskState{
    public const string Idle = "animation";
    public const string Attack = "animation2";
    public const string Die = "animation3";
}