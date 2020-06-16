using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMonsterMoveControl : MonoBehaviour
{
    #region Fields
    public GameObject target;
    public ViewRange viewRange;
    public float speed;
    public float endPos;
    public float followSpeed;
    public bool isLoopMove = true;

    Vector3 facingLeft = new Vector3(-1,1,1);
    Vector3 facingRight = new Vector3(1,1,1);
    #endregion Fields

        
    #region Moving And Facing
    public void MonsterLoopMove(float startPos){
        if(viewRange.onEnter == false){
            isLoopMove = true;
            transform.position = new Vector3(Mathf.Lerp(startPos,endPos,Mathf.PingPong(Time.time,1)),transform.position.y);
        }
        FacingWhileLoop(startPos);
    }
    public void FacingWhileLoop(float startPos){
        if(startPos < endPos){
            if(this.transform.position.x == (startPos + endPos)){
                this.transform.localScale = facingLeft;
            } 
            if(this.transform.position.x >= startPos){
                this.transform.localScale = facingRight;
            }
        }
        if(startPos > endPos){
            if(this.transform.position.x == (startPos + endPos)){
                this.transform.localScale = facingLeft;
            }
            if(this.transform.position.x >= endPos){
                this.transform.localScale = facingRight;
            }
        }
    }
    public void MonsterChasing(){
        isLoopMove = false;
        LookAtPlayer();
        transform.position = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
    }
    public void LookAtPlayer(){
        if(this.transform.position.y <= target.transform.position.y){
            this.transform.localScale = facingRight;
        } else this.transform.localScale = facingLeft;
    }
    #endregion Moving And Facing

    
}
