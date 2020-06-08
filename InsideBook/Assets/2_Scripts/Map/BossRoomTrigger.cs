using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public Transform CameraPosition;
    public float orthoSize = 5.6f;
    public float tweenTime = 3.0f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CameraSetting.Instance.SwitchToBossRoomState(CameraPosition.position, orthoSize, tweenTime);
        }
    }
}
