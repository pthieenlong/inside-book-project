using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Rigidbody2D _Object;
    public float speed = 20f;
    //public float begin, end;
    //public bool isMainCharacter = false;
    void Start()
    {
        _Object.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if (isMainCharacter)
        
            // MovingLeft();
            // if(Input.GetKeyDown(KeyCode.A)){
            //     Debug.Log("a");
            // }   
    }
    public void ObjectMoving() //object preriod moving
    { 
        
        // if(gameObject.transform.position.x = begin)
        // {
        //    MovingRight();
        // }
        // if(gameObject.transform.position.x = end) {
        //    MovingLeft();
        // }
    }
    public void MovingRight() {
        _Object.velocity = Vector2.right * speed * Time.deltaTime;
    }
    public void MovingLeft()
    {
        _Object.velocity = Vector2.left * speed * Time.deltaTime;
    }
}