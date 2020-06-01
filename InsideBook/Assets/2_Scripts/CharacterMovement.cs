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
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("a");
            //playerAnim.Play(SaveKey.Left);
            
            rb.velocity = Vector2.left * speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            Debug.Log("d");
            //playerAnim.Play(SaveKey.Right);
            rb.velocity = Vector2.right * speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("space");
            rb.velocity = new Vector2(0, jumpForce);
            
        }
    }
}
public class SaveKey
{
    public const String Left = "LeftTrigger";
    public const String Right = "RightTrigger";
    
}


