using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive = false;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteActive;
    public Sprite spriteDeActive;

    void Start()
    {
        if (isActive)
            OnActive();
        else
            OnDeactive();
    }

    public void OnActive()
    {
        if (GamePlaySetting.CurrentCheckPoint)
            GamePlaySetting.CurrentCheckPoint.OnDeactive();
        GamePlaySetting.CurrentCheckPoint = this;
        spriteRenderer.sprite = spriteActive;
    }

    public void OnDeactive()
    {
        spriteRenderer.sprite = spriteDeActive;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (isActive == false)
            {
                OnActive();
            }
        }
    }
}
