using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : MonoBehaviour
{
    public GameObject target;
    public FlyingMaskController AnimMask;
    public ViewRange viewRange;
    public AttackRange attackRange;
    public float speed;
    public float moveLength;
    public float followSpeed;
    public float coolDownTime;
    
    bool isAttack = false;
    //public Vector3 overDistance = new Vector3(5,5);
    //MoveObject moveObject;
    // public bool isFollow = false;
    void Update()
    {
        MonsterLoopMove();
    }
    void LateUpdate(){
        if(viewRange.isFollow == true){
            //transform.LookAt(target.transform);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
        }
        if(GamePlaySetting.IsDead == true){
            viewRange.isFollow = false;
        }
    }

    public void MonsterLoopMove(){
        if(viewRange.isFollow == false)
        transform.position = new Vector2(Mathf.PingPong(Time.time, moveLength), transform.position.y);
    }
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true){
            AnimMask.SetAnimation(MonsterState.Attack,false);
            isAttack = true;
            AttackCooldown();
        }
    }
    public void AttackCooldown(){
        if(isAttack){
            coolDownTime -= Time.deltaTime;
            isAttack = false;
        }
    }
}
