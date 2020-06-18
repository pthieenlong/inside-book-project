using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public int AttackPower = 1;
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("other.transform:" + other.transform.tag);
        if (this.transform.CompareTag("PlayerAttack") && other.transform.CompareTag("Monster"))
        {
            other.transform.parent.GetComponent<IMonsterControl>().GetHit(AttackPower);
        }
    }
}
