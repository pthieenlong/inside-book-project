using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(this.transform.CompareTag("PlayerAttack") && other.transform.CompareTag("Monster"))
        {
            other.GetComponent<IMonsterControl>().GetHit();
        }
    }
}
