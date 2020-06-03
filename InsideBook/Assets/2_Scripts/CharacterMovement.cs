using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MoveObject moveController;
    public LilyAnimControl AnimLily;

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
            if (!AnimLily.isJumping)
                AnimLily.SetAnimation(LilyState.Move);
            AnimLily.SetDirection(false);
        }
        else if (moveDirection < 0) // Left
        {
            moveController.MovingLeft();
            if (!AnimLily.isJumping)
                AnimLily.SetAnimation(LilyState.Move, true);
            AnimLily.SetDirection(true);
        }
        else
        {
            if (!AnimLily.isJumping)
                AnimLily.SetAnimation(LilyState.Idle);
        }
        #endregion Horizontal

        #region Vertical
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            // moveController.Jump();
            AnimLily.isJumping = true;
            StartCoroutine(JumpRoutine());
        }
        #endregion Vertical
    }

    float jumpTimeScale = 1;
    IEnumerator JumpRoutine()
    {
        jumpTimeScale = 3;
        AnimLily.SetAnimation(LilyState.Jump_Start, false, jumpTimeScale);
        yield return new WaitForSeconds(0.467f / jumpTimeScale);
        AnimLily.SetAnimation(LilyState.Jumping, false);
        moveController.Jump();
        //TODO state setting here
    }
}
public class SaveKey
{
    public const String Left = "LeftTrigger";
    public const String Right = "RightTrigger";

}


