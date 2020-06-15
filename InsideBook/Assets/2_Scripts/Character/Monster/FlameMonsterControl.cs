using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameMonsterControl : SpineAnimControl
{
    public MonsterSystem monsterSystem;
    float startPos;
    void Start(){
        startPos = this.transform.position.x;

        SetAnimation(FlameState.Idle,true);
    }
    void Update(){
        if(monsterSystem.isAttack == false){
            SetAnimation(SoulBatState.Idle, true);
        }
        
        if(monsterSystem.isLoopMove){
            monsterSystem.MonsterLoopMove(startPos);
        }
        monsterSystem.MonsterAttack();
        if(monsterSystem.isAttack == true){
            SetAnimation(SoulBatState.Attack, false);
        }
        
    }
    void LateUpdate(){
        monsterSystem.LookAtPlayer();
        if(monsterSystem.viewRange.isFollow == true){
            monsterSystem.MonsterChasing();

        }
        if(GamePlaySetting.IsDead == true){
            SetAnimation(SoulBatState.Die, false);
            monsterSystem.viewRange.isFollow = false;

        } 
    }
}
public class FlameState
{
    public const string Idle = "tho";
    public const string Attack = "skill";
    public const string Die = "hit";
}

