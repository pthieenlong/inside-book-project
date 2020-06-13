using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : MonoBehaviour
{
    public GameObject target;
    public FlyingMaskController AnimMask;
    public float speed;
    public float moveLength;
    public float followSpeed;
    public Vector3 overDistance = new Vector3(5,5);
    bool isFollow = false;
    float moveDirection = 0;
    void Update()
    {
        MonsterLoopMove();
    }
    void LateUpdate(){
        if(isFollow == true){
            transform.position = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
        }
    }
    public void MonsterLoopMove(){
        if(isFollow == false)
        transform.position = new Vector2(Mathf.PingPong(Time.time, moveLength), transform.position.y);
    }
    
    void OnTriggerEnter2D(Collider2D viewRange){
        if(viewRange.transform.CompareTag("Player")){
            isFollow = true;
        }
    }
    void OnTriggerExit2D(Collider2D viewRange){
        isFollow = false;
    }
}
