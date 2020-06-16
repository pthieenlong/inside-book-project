using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool isOnAttackRange;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.CompareTag("Player")){
            isOnAttackRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        isOnAttackRange = false;
    }
    
}
