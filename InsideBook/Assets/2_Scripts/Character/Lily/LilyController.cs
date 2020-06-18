using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LilyController : MonoBehaviour
{
    public bool canControl = true;
    public LilyMoveControl moveController;
    public LilyAnimControl AnimLily;
    public LayerMask LayerGround;
    float moveDirection = 0;

    void Update()
    {
        if (!GamePlaySetting.IsDead)
        {
            if (canControl && moveController.canMove)
                PlayerCharacterControl();

            ApplyMovement();
        }
    }
    public void PlayerCharacterControl()
    {
        if (!canControl)
            return;

        #region Horizontal
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            // moveDirection = 0;
            UIJoystick.Instance.OnPointerUp();
        }

        if (Input.GetKey(KeyCode.A))
        {
            // moveDirection = -1;
            UIJoystick.Instance.MoveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            // moveDirection = 1;
            UIJoystick.Instance.MoveRight();
        }

        moveDirection = UIJoystick.JoystickDirection.x;
        #endregion Horizontal

        #region Vertical
        if (Input.GetKey(KeyCode.W))
        {
            moveController.dashVector.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveController.dashVector.y = -1;
        }
        else
            moveController.dashVector.y = 0;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            UseJump();
        }
        #endregion Vertical

        #region Attack
        //Dash
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseDash();
        }
        #endregion Attack
    }

    public void ApplyMovement()
    {
        if (!moveController.isAutoMove)
        {
            if (moveDirection > 0)      // Right
            {
                moveController.MovingRight();
                if (!AnimLily.isJumping && IsGrounded())
                    AnimLily.SetAnimation(LilyState.Move, true, 1.2f);
            }
            else if (moveDirection < 0) // Left
            {
                moveController.MovingLeft();
                if (!AnimLily.isJumping && IsGrounded())
                    AnimLily.SetAnimation(LilyState.Move, true, 1.2f);
            }
            else
            {
                if (!AnimLily.isJumping && IsGrounded() && !moveController.isDashing)
                    AnimLily.SetAnimation(LilyState.Idle);
            }
        }
        if (!IsGrounded())
        {
            if (moveController._Object.velocity.y < 0)
            {
                AnimLily.SetAnimation(LilyState.Falling, false);
            }
        }
    }

    public void AutoMoveTo(Vector3 des, float duration)
    {
        moveDirection = 0;
        AnimLily.SetAnimation(LilyState.Move, true, 1.2f);
        float direction = (des - moveController._Object.transform.position).x;
        bool isLeft = direction / Mathf.Abs(direction) < 0;
        moveController.AutoMoveByTime(isLeft, Mathf.Abs(direction) / duration, duration);
    }

    public void UseJump()
    {
        if (IsGrounded() && !AnimLily.isJumping)
        {
            AnimLily.isJumping = true;
            StartCoroutine(JumpRoutine());
        }
    }
    public void UseDash()
    {
        if (IsGrounded() || moveController.dashCount > 0)
        {
            moveController.StartDash();
            moveDirection = 0;
        }
    }

    public float raycastRange = 0.3f;
    RaycastHit2D hitObject;
    bool IsGrounded()
    {

        hitObject = Physics2D.Raycast(this.transform.position, Vector2.down, raycastRange, (int)LayerGround);
        if (hitObject)
        {
            if (hitObject.transform.CompareTag("Ground"))
            {
                if (AnimLily.currentState == LilyState.Falling
                 || AnimLily.currentState == LilyState.Jumping
                 && moveController._Object.velocity.y < 0)
                {
                    AnimLily.isJumping = false;
                    moveController.dashCount = GamePlaySetting.BaseDashCount;
                }
                return true;
            }
        }
        return false;
    }

    public float jumpTimeScale = 3;
    IEnumerator JumpRoutine()
    {
        AnimLily.SetAnimation(LilyState.Jump_Start, false, jumpTimeScale);
        yield return new WaitForSeconds(0.467f / jumpTimeScale);
        AnimLily.SetAnimation(LilyState.Jumping, false);
        moveController.Jump();
        //TODO state setting here
    }

    public void OnDead()
    {
        canControl = false;
        GamePlaySetting.IsDead = true;
        moveController._Object.velocity = Vector3.zero;
        moveController._Object.isKinematic = true;
        moveDirection = 0;
        AnimLily.SetAnimation(LilyState.Dead, false);
        Invoke("FadeOut", 1.1f);
        Invoke("OnReSpawn", 2);
    }

    void FadeOut()
    {
        UIControl.Instance.FadeOut();
    }

    public void OnReSpawn()
    {

        CameraSetting.Instance.SwitchToNormalState(5f);
        UIControl.Instance.FadeIn();
        canControl = true;
        GamePlaySetting.IsDead = false;
        moveController._Object.isKinematic = false;
        moveController._Object.velocity = Vector3.zero;
        moveDirection = 0;
        UIJoystick.Instance.OnPointerUp();
        this.transform.position = GamePlaySetting.CurrentCheckPoint.transform.position;

        if (GamePlaySetting.OnRespawn != null)
            GamePlaySetting.OnRespawn();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (moveController.canDead && other.transform.CompareTag("Dead"))
        {
            if (!GamePlaySetting.IsDead)
                OnDead();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, Vector2.down * raycastRange);
    }
}


