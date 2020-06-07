using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Rigidbody2D _Object;
    public float speed = 20f;
    public float jumpForce = 10f;
    public float dashForce = 30f;
    public Vector2 dashVector;
    bool isDashing = false;
    float moveDirection = 1;
    float gravityScale = 5f;

    void Start()
    {
        _Object.GetComponent<Rigidbody>();
    }
    
    void Update(){
        if(isDashing){
            Dashing();
        }
    }
    public void StopMoving()
    {
        _Object.velocity = _Object.velocity.V2SetX(0);
    }

    public void MovingRight()
    {
        // _Object.velocity = Vector2.right * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.right.x * speed);
        moveDirection = 1;
    }
    public void MovingLeft()
    {
        // _Object.velocity = Vector2.left * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.left.x * speed);
        moveDirection = -1;
    }

    public void Jump()
    {
        _Object.velocity = _Object.velocity.V2SetY(jumpForce);
    }
    public void StartDash(){
        isDashing = true;
        gravityScale = _Object.gravityScale;
        _Object.gravityScale = 0;
        // _Object.velocity = dashVector;
        _Object.velocity = _Object.velocity.V2SetX(dashForce * moveDirection);
    }
    public void Dashing(){
        _Object.velocity = Vector2.Lerp(_Object.velocity, Vector2.zero, 0.25f);
        if(Mathf.Abs(_Object.velocity.x) < 1){
            EndDash();
        }
    }
    public void EndDash(){
        isDashing = false;
        _Object.gravityScale = gravityScale;
    }
    


    void OnDrawGizmos()
    {
        Gizmos.DrawRay(_Object.transform.position, _Object.velocity);
    }
    
}