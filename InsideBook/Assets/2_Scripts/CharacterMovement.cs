using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MoveObject moveController;
    public LilyAnimControl AnimLily;
    public LayerMask LayerGround;

    float moveDirection = 0;
    void Update()
    {
        PlayerCharacterMovement();
    }
    public void PlayerCharacterMovement()
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

        if (moveDirection > 0)      // Right
        {
            moveController.MovingRight();
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Move);
            AnimLily.SetDirection(false);
        }
        else if (moveDirection < 0) // Left
        {
            moveController.MovingLeft();
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Move, true);
            AnimLily.SetDirection(true);
        }
        else
        {
            if (!AnimLily.isJumping && IsGrounded())
                AnimLily.SetAnimation(LilyState.Idle);
        }
        #endregion Horizontal

        #region Vertical
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
        // else
        // {
        //     if (AnimLily.currentState == LilyState.Falling)
        //     {
        //         AnimLily.SetAnimation(LilyState.Fall_IsGrounded, false, 3);
        //     }
        // }
        #endregion Vertical
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
                    AnimLily.isJumping = false;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, Vector2.down * raycastRange);
    }
}
public class SaveKey
{
    public const String Left = "LeftTrigger";
    public const String Right = "RightTrigger";

}


