using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterController : MonoBehaviour, IMonsterControl
{
    public Transform target;
    [Header("Status")]
    public int MaxHP = 1;
    int hp = 1;
    public int HP
    {
        get { return this.hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                OnDead();
            }
        }
    }

    [Header("Respawn")]
    public bool isRespawnable = true;
    public float RespawnTime = 10;
    public Transform RespawnPosition;

    void Start()
    {
        HP = MaxHP;
    }

    public virtual void OnDead()
    {
        if (isRespawnable)
        {
            Invoke("OnRespawn", RespawnTime);
        }
    }

    public virtual void OnRespawn()
    { }
    public virtual void GetHit(int dmg)
    {
        HP -= dmg;
    }
    public virtual void Detect() { }
    public virtual void Attack() { }
}
