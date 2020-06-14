using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool isOnAttackRange;
    void OnTriggerEnter2D(){
        if(this.transform.CompareTag("Player")){
            isOnAttackRange = true;
            Debug.Log("is on attack");
        }
    }
    void OnTriggerExit2D(){
        isOnAttackRange = false;
    }
    
}
