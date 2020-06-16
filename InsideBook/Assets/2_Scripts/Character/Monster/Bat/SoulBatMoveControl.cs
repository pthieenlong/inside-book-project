using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBatMoveControl : MonoBehaviour
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
