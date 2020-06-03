using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Rigidbody2D _Object;
    public float speed = 20f;
    public float jumpForce = 10f;

    void Start()
    {
        _Object.GetComponent<Rigidbody>();
    }

    public void StopMoving()
    {
        _Object.velocity = _Object.velocity.V2SetX(0);
    }

    public void MovingRight()
    {
        // _Object.velocity = Vector2.right * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.right.x * speed);
    }
    public void MovingLeft()
    {
        // _Object.velocity = Vector2.left * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.left.x * speed);
    }

    public void Jump()
    {
        _Object.velocity = _Object.velocity.V2SetY(jumpForce);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(_Object.transform.position, _Object.velocity);
    }
}