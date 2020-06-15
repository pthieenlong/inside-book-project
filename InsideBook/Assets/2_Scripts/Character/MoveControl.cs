using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveControl : MonoBehaviour
{
    [Header("Moving")]
    public bool canMove = true;
    public Rigidbody2D _Object;
    public GameObject ModelObj;
    public float speed = 20f;
    public float jumpForce = 10f;
    public bool isAutoMove = false;

    float moveDirection = 1;
    float baseDirection = 1;

    void Start()
    {
        _Object.GetComponent<Rigidbody>();
        isAutoMove = false;
    }

    #region Moving
    public void AutoMoveByTime(bool is_left, float speed, float duration)
    {
        isAutoMove = true;
        StartCoroutine(AutoMoveRoute(duration, is_left));
    }
    IEnumerator AutoMoveRoute(float duration, bool is_left)
    {
        float dur = duration;
        while (dur > 0)
        {
            dur -= Time.deltaTime;
            if (is_left)
                MovingLeft();
            else
                MovingRight();
            yield return Time.deltaTime;
        }

        isAutoMove = false;
        UIJoystick.Instance.OnPointerUp();
        StopMoving();
        yield return null;
    }
    public void StopMoving()
    {
        _Object.velocity = _Object.velocity.V2SetX(0);
    }
    public void MovingRight(float v = 0)
    {
        if (v == 0)
            _Object.velocity = _Object.velocity.V2SetX(Vector2.right.x * speed);
        else
            _Object.velocity = _Object.velocity.V2SetX(Vector2.right.x * v);
        moveDirection = 1;
        SetDirection(false);
    }
    public void MovingLeft(float v = 0)
    {
        if (v == 0)
            _Object.velocity = _Object.velocity.V2SetX(Vector2.left.x * speed);
        else
            _Object.velocity = _Object.velocity.V2SetX(Vector2.left.x * v);
        moveDirection = -1;
        SetDirection(true);
    }
    public void Jump()
    {
        _Object.velocity = _Object.velocity.V2SetY(jumpForce);
    }
    #endregion Moving


    #region Helper Methods
    public void SetDirection(bool isLeft)
    {
        _Object.transform.localScale = _Object.transform.localScale.V3SetX(baseDirection * (isLeft ? -1 : 1));
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(_Object.transform.position, _Object.velocity);
    }
    #endregion Helper Methods
}