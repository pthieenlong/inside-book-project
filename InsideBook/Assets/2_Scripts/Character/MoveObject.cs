using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    [Header("Moving Fields")]
    public bool canMove = true;
    public Rigidbody2D _Object;
    public GameObject ModelObj;
    public float speed = 20f;
    public float jumpForce = 10f;

    [Header("Dashing Fields")]
    public Vector2 dashVector;
    public Animator DashFX;
    public Animator SplashFx;
    public float dashForce = 30f;
    public float dashTime = 0.25f;
    public int dashCount = 1;
    public bool isDashing = false;

    float moveDirection = 1;
    float gravityScale = 6f;
    float currentScale = 0.15f;
    float scaleTime = 0.2f;
    float timeCount = 0;
    float baseDirection = 1;
    void Start()
    {
        _Object.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDashing)
        {
            Dashing();
        }
    }

    #region Moving
    public void StopMoving()
    {
        _Object.velocity = _Object.velocity.V2SetX(0);
    }

    public void MovingRight()
    {
        // _Object.velocity = Vector2.right * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.right.x * speed);
        moveDirection = 1;
        SetDirection(false);
    }
    public void MovingLeft()
    {
        // _Object.velocity = Vector2.left * speed * Time.deltaTime;
        _Object.velocity = _Object.velocity.V2SetX(Vector2.left.x * speed);
        moveDirection = -1;
        SetDirection(true);
    }

    public void Jump()
    {
        _Object.velocity = _Object.velocity.V2SetY(jumpForce);
    }
    #endregion Moving

    #region Dashing Skill
    public void StartDash()
    {
        dashCount--;
        timeCount = 0;
        gravityScale = _Object.gravityScale;
        _Object.gravityScale = 0;
        currentScale = ModelObj.transform.parent.localScale.z;
        _Object.velocity = Vector3.up * 5f;
        canMove = false;
        DashFX.Play("StartDash");

        ModelObj.transform.parent.DOScale(Vector3.one * currentScale * 0.25f, scaleTime).OnComplete(() =>
        {
            isDashing = true;
            // _Object.velocity = _Object.velocity.V2SetX(dashForce * moveDirection);
            _Object.velocity = (dashVector * dashForce);
            PlaySplashFX();
            // DashFX.gameObject.SetActive(false);
        });
    }
    public void Dashing()
    {
        _Object.velocity = Vector2.Lerp(_Object.velocity, Vector2.zero, 0.05f);

        timeCount += Time.deltaTime;
        if (timeCount >= dashTime)
        {
            EndDash();
        }
    }
    public void EndDash()
    {
        isDashing = false;
        _Object.velocity = Vector3.zero;
        DashFX.Play("EndDash");

        ModelObj.transform.parent.DOScale(Vector3.one * currentScale, scaleTime).OnComplete(() =>
        {
            canMove = true;
            _Object.gravityScale = gravityScale;
        });
    }

    void PlaySplashFX()
    {
        SplashFx.transform.position = ModelObj.transform.parent.position;
        SplashFx.transform.right = _Object.velocity.normalized;
        // SplashFx.transform.gameObject.SetActive(true);
        SplashFx.Play("WaterSplash");
    }
    #endregion Dashing Skill

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