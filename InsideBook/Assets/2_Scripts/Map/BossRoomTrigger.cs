using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossRoomTrigger : MonoBehaviour
{
    [Header("Characters")]
    public TheGhostControl TheGhost;
    public LilyController Lily;
    public Transform EnterPosition;

    [Header("Camera")]
    public Transform CameraPosition;
    public float orthoSize = 5.6f;
    public float tweenTime = 3.0f;

    [Header("Other")]
    public GameObject RockPillar;

    void Start()
    {
        GamePlaySetting.OnRespawn -= Respawn;
        GamePlaySetting.OnRespawn += Respawn;
    }

    void Respawn()
    {
        RockPillar.gameObject.SetActive(false);
        TheGhost.Init();
    }

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
                UIJoystick.Instance.OnPointerUp();
                RockPillar.gameObject.SetActive(true);
            });

            // boss raise
            DOVirtual.DelayedCall(tweenTime + 2, () =>
            {
                TheGhost.Raise();
                UIControl.Instance.ShowUI();
                Lily.canControl = true;
            });
        }
    }
}
