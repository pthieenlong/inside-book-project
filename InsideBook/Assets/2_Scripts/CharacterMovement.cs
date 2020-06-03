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
            Debug.Log("a");
            moveDirection = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("d");
            moveDirection = 1;
        }

        if (moveDirection > 0)      // Right
        {
            moveController.MovingRight();
            AnimLily.SetAnimation(LilyState.Move);
            AnimLily.SetDirection(false);
        }
        else if (moveDirection < 0) // Left
        {
            moveController.MovingLeft();
            AnimLily.SetAnimation(LilyState.Move, true);
            AnimLily.SetDirection(true);
        }
        else
        {
            AnimLily.SetAnimation(LilyState.Idle);
        }
        #endregion Horizontal

        #region Vertical
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            moveController.Jump();
        }
        #endregion Vertical
    }
}
public class SaveKey
{
    public const String Left = "LeftTrigger";
    public const String Right = "RightTrigger";

}


