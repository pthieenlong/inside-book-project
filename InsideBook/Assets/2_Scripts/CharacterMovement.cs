using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    //public Animator playerAnim;
    public float speed = 300f;
    public float jumpForce = 10f;
    public bool isJump = false;
    float moveDirection = 0;
    void Start()
    {
        rb.GetComponents<Rigidbody2D>();
    }
    void Update()
    {
        PlayerCharacterMovement();
        //Input.Get
        // if(Input.GetKeyDown(KeyCode.A)){
        //     //Debug.Log("a");
        //     playerAnim.Play(SaveKey.Left);
        //     rb.velocity = Vector2.left * speed * Time.deltaTime;
        // }
        // if(Input.GetKeyDown(KeyCode.D)){
        //     //Debug.Log("d");
        //     playerAnim.Play(SaveKey.Right);
        //     rb.velocity = Vector2.right * speed * Time.deltaTime;
        // }
    }
    public void PlayerCharacterMovement()
    {
        #region Horizontal
        // if(Input.GetKeyDown(KeyCode.A)){
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            moveDirection = -1;
            //playerAnim.Play(SaveKey.Left);
            // rb.velocity = Vector2.left * speed * Time.deltaTime;
        }
        // if(Input.GetKeyDown(KeyCode.D)){
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("d");
            moveDirection = 1;
            //playerAnim.Play(SaveKey.Right);
            // rb.velocity = Vector2.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            moveDirection = 0;
        }

        rb.velocity = rb.velocity.V2SetX(moveDirection * speed * Time.deltaTime);
        #endregion Horizontal

        #region Vertical
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("space");
            // rb.velocity = new Vector2(0, jumpForce);
            // if (rb.IsTouchingLayers(LayerMask.NameToLayer("Ground"))) // chua dung, check tam thoi
            // {
            rb.velocity = rb.velocity.V2SetY(jumpForce);
            // }
        }
        #endregion Vertical
    }
}
public class SaveKey
{
    public const String Left = "LeftTrigger";
    public const String Right = "RightTrigger";

}


