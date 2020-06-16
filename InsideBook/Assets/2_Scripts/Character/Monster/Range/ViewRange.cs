using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{
    public bool onEnter;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.CompareTag("Player")){
            onEnter = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        onEnter = false;
    }
}
