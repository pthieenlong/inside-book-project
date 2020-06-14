using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{
    public bool isFollow;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.CompareTag("Player")){
            isFollow = true;
            Debug.Log("is follow");
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        isFollow = false;
    }
}
