using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : MonoBehaviour
{
    public GameObject target;
    public ViewRange viewRange;
    public AttackRange attackRange;
    public float speed;
    public float endPos;
    // public float maxLength;
    // public float minLength;
    public float followSpeed;
    public float countDownTime;
    public bool isAttack = false;
    public bool isLoopMove = true;

    //public float counter;
    float timeTemp;
    Vector3 facingLeft = new Vector3(-1,1,1);
    Vector3 facingRight = new Vector3(1,1,1);
    
    //public Vector3 overDistance = new Vector3(5,5);
    //MoveObject moveObject;
    // public bool isFollow = false;
        // void Start(){
        //     timeTemp = countDownTime;
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
    public void MonsterLoopMove(float startPos){
        if(viewRange.isFollow == false){
            //Debug.Log("is loop");
            isLoopMove = true;
            transform.position = new Vector3(Mathf.Lerp(startPos,endPos,Mathf.PingPong(Time.time,1)),transform.position.y);//(min, max, Mathf.Pingpong(Time.time, 1));
        }
        FacingWhileLoop(startPos);
    }
    public void FacingWhileLoop(float startPos){
        if(startPos < endPos){
            if(this.transform.position.x == endPos){
                this.transform.localScale = facingLeft;
            } 
            if(this.transform.position.x >= startPos){
                this.transform.localScale = facingRight;
            }
        }
        if(startPos > endPos){
            if(this.transform.position.x == startPos){
                this.transform.localScale = facingLeft;
            }
            if(this.transform.position.x >= endPos){
                this.transform.localScale = facingRight;
            }
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
     countDownTime -= Time.deltaTime;
        if(countDownTime <= 0){
            EndAttack();
        }
    }
    public void EndAttack(){
        isAttack = false;
        //Anim.SetAnimation(state,isLoop);
     countDownTime = timeTemp;
    }
    #endregion Attack
}
