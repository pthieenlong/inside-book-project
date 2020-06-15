using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LilyMoveControl : MoveControl
{
    #region Fields
    [Header("Dashing")]
    public Vector2 dashVector;
    public Animator DashFX;
    public Animator SplashFx;
    public float dashForce = 30f;
    public float dashTime = 0.25f;
    public int dashCount = 1;
    public bool isDashing = false;

    float gravityScale = 6f;
    float currentScale = 0.15f;
    float scaleTime = 0.2f;
    float timeCount = 0;
    #endregion Fields

    void Update()
    {
        if (isDashing)
        {
            Dashing();
        }
    }

    #region Dashing Skill
    public void StartDash()
    {
        dashCount--;
        timeCount = 0;
        currentScale = ModelObj.transform.parent.localScale.z;
        gravityScale = _Object.gravityScale;
        _Object.gravityScale = 0;
        _Object.velocity = Vector3.up * 5f;
        canMove = false;
        DashFX.Play("StartDash");

        ModelObj.transform.parent.DOScale(Vector3.one * currentScale * 0.25f, scaleTime).OnComplete(() =>
        {
            isDashing = true;
            dashVector = UIJoystick.JoystickDirection;
            if (dashVector.x == 0)
                dashVector.x = _Object.transform.localScale.x;
            _Object.velocity = (dashVector * dashForce);
            PlaySplashFX();
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
        UIJoystick.Instance.OnPointerUp();
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
        SplashFx.Play("WaterSplash");
    }
    #endregion Dashing Skill
}
