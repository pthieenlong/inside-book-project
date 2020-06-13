using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossRoomTrigger : MonoBehaviour
{
    [Header("Character")]
    public CharacterMovement Lily;
    public Transform EnterPosition;

    [Header("Camera")]
    public Transform CameraPosition;
    public float orthoSize = 5.6f;
    public float tweenTime = 3.0f;

    [Header("Other")]
    public GameObject RockPillar;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //hide ui / lock control
            UIControl.Instance.HideUi();
            Lily.canControl = false;

            // switch camera
            CameraSetting.Instance.SwitchToBossRoomState(CameraPosition.position, orthoSize, tweenTime);

            //move lily
            Lily.AutoMoveTo(EnterPosition.position, tweenTime / 3);
            DOVirtual.DelayedCall(tweenTime / 3, () =>
              {
                  // block door
                  RockPillar.gameObject.SetActive(true);
                  UIJoystick.Instance.OnPointerUp();
              });

            // boss raise
            // show ui / unlock control
        }
    }
}
