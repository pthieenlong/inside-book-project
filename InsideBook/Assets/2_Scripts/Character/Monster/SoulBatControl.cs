using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBatControl : SpineAnimControl
{
    float startPos;
    public MonsterSystem monsterSystem;
    void Start(){
        startPos = this.transform.position.x;
        SetAnimation(SoulBatState.Idle,true);
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
        if(monsterSystem.viewRange.isFollow == true){
            monsterSystem.MonsterChasing();
            monsterSystem.LookAtPlayer();
        }
        if(GamePlaySetting.IsDead == true){
            SetAnimation(SoulBatState.Die, false);
            monsterSystem.viewRange.isFollow = false;

        } 
    }
}
public class SoulBatState{
    public const string Idle = "tho";
    public const string Attack = "skill";
    public const string Die = "die";
}

