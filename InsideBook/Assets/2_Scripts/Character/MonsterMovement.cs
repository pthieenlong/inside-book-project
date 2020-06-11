using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Collider2D Physics;
    public FlyingMaskController AnimMask;
    public MoveObject moveController;
    public float speed;
    public float moveLength;
    
    float moveDirection = 0;
    void Update()
    {
        MonsterLoopMove();
    }
    public void MonsterMoving(){
    #region Moving
        if (Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.O))
        {
            moveDirection = 0;
        }

        if (Input.GetKey(KeyCode.O))
        {
            //moveDirection = 1;
            moveController._Object.velocity = Vector2.right * speed;
        }

        if (Input.GetKey(KeyCode.I))
        {
            //moveDirection = -1;
            //moveController.MovingLeft();
            moveController._Object.velocity = Vector2.left * speed;
        }
    #endregion Moving

    #region Attack
        if(Input.GetKeyDown(KeyCode.M)){
            AnimMask.SetAnimation(MonsterState.Attack,false);
        }
    #endregion Attack
    }
    public void MonsterLoopMove(){
        // Vector3 v = startPos;
        // v.x = delta * Mathf.Sin(Time.time * speed);
        // transform.position = v;
        transform.position = new Vector2(Mathf.PingPong(Time.time, moveLength), transform.position.y);
    }
    void OnTriggerView(Collider viewRange){
        if(viewRange.transform.CompareTag("Player")){
            Debug.Log("a");
        }
    }

    
}
