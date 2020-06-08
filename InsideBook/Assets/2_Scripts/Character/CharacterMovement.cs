using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool canControl = true;
    public MoveObject moveController;
    public LilyAnimControl AnimLily;
    public LayerMask LayerGround;
    float moveDirection = 0;

    void Update()
    {
        if (moveController.canMove && !GamePlaySetting.IsDead)
            PlayerCharacterControl();
    }
    public void PlayerCharacterControl()
    {
        #region Horizontal

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            moveDirection = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1;
        }

        moveController.dashVector.x = moveController._Object.transform.localScale.x;

        if (moveDirection > 0)      // Right
        {
            moveController.MovingRight();
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Move);
        }
        else if (moveDirection < 0) // Left
        {
            moveController.MovingLeft();
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Move, true);
        }
        else
        {
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Idle);
        }

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
            if (IsGrounded() && !AnimLily.isJumping)
            {
                AnimLily.isJumping = true;
                StartCoroutine(JumpRoutine());
            }
        }

        if (!IsGrounded())
        {
            if (moveController._Object.velocity.y < 0)
            {
                AnimLily.SetAnimation(LilyState.Falling, false);
            }
        }
        #endregion Vertical

        #region Attack
        //Dash
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (IsGrounded() || moveController.dashCount > 0)
            {
                moveController.StartDash();
                moveDirection = 0;
            }
        }
        #endregion Attack
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
        moveController._Object.isKinematic = true;
        moveController._Object.velocity = Vector3.zero;
        AnimLily.SetAnimation(LilyState.Dead, false);
        Invoke("OnReSpawn", 2);
    }

    public void OnReSpawn()
    {
        canControl = true;
        GamePlaySetting.IsDead = false;
        moveController._Object.isKinematic = false;
        moveDirection = 0;
        this.transform.position = GamePlaySetting.CurrentCheckPoint.transform.position;
        CameraSetting.Instance.SwitchToNormalState(5f);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Dead"))
        {
            OnDead();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, Vector2.down * raycastRange);
    }
}


