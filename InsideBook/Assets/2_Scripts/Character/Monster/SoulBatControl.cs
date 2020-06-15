using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBatControl : SpineAnimControl
{
    public MonsterSystem monsterSystem;
    void Start()
    {
        SetAnimation(SoulBatState.Idle, true);
    }
    void Update(){
        monsterSystem.LookAtPlayer();
        monsterSystem.MonsterLoopMove();
        monsterSystem.MonsterAttack(SoulBatState.Attack, false);
        if(monsterSystem.isAttack == false){
            SetAnimation(SoulBatState.Idle);
        }
    }
    void LateUpdate(){
        if(monsterSystem.viewRange.isFollow == true){
            //transform.LookAt(target.transform);
            monsterSystem.MonsterChasing();
        }
        if(GamePlaySetting.IsDead == true){
            monsterSystem.viewRange.isFollow = false;
        } 
    }
}
public class SoulBatState{
    public const string Idle = "tho";
    public const string Attack = "skill";
    public const string Die = "die";
}

