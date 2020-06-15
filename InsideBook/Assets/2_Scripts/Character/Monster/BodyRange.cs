using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRange : MonoBehaviour
{
    public bool isAttack;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.CompareTag("Player")){
            isAttack = true;
            Debug.Log("it's attack");
        }
    }
}
