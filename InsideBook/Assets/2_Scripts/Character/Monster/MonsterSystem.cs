using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : MonoBehaviour
{
    public GameObject target;
    public ViewRange viewRange;
    public AttackRange attackRange;
    public float speed;
    public float moveLength;
    public float followSpeed;
    public float coolDownTime;
    public bool isAttack = false;
    public bool isLoopMove = false;
    float timeTemp;
    Vector3 facingLeft = new Vector3(-1,1,1);
    Vector3 facingRight = new Vector3(1,1,1);
    //public Vector3 overDistance = new Vector3(5,5);
    //MoveObject moveObject;
    // public bool isFollow = false;
        // void Start(){
        //     timeTemp = coolDownTime;
        // }
        // void Update()
        // {
        //     MonsterAttack();

        //     LookAtPlayer();
        //     MonsterLoopMove();
        // }
        // void LateUpdate(){
        //     if(viewRange.isFollow == true){
        //         //transform.LookAt(target.transform);
        //         MonsterChasing();
        //     }
        //     if(GamePlaySetting.IsDead == true){
        //         viewRange.isFollow = false;
        //     } 
        // }
        
#region Moving And Facing
    public void MonsterLoopMove(){
        if(viewRange.isFollow == false){
            isLoopMove = true;
            transform.position = new Vector2(Mathf.PingPong(Time.time, moveLength), transform.position.y);
        }
    }
    public void MonsterChasing(){
        isLoopMove = false;
        transform.position = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
    }
    public void LookAtPlayer(){
        if(this.transform.position.y <= target.transform.position.y){
            this.transform.localScale = facingRight;
        } else this.transform.localScale = facingLeft;
    }
    #endregion Moving And Facing

    #region Attack
    public void MonsterAttack(){
        if(attackRange.isOnAttackRange == true && isAttack == false) {
            isAttack = true;
        }
            AttackCooldown();
    }
    public void AttackCooldown(){
        coolDownTime -= Time.deltaTime;
        if(coolDownTime <= 0){
            EndAttack();
        }
    }
    public void EndAttack(){
        isAttack = false;
        //Anim.SetAnimation(state,isLoop);
        coolDownTime = timeTemp;
    }
    #endregion Attack
}
