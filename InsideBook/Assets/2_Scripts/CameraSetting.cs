using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSetting : MonoBehaviour
{
    public static CameraSetting Instance;
    public Camera MainCamera;
    public CameraState cameraState = CameraState.Normal;
    public GameObject target;
    public float cameraSpeed = 0.125f;
    public Vector3 offset;
    public float xRightLimit;
    public float xLeftLimit;
    public float yUpLimit;
    public float yDownLimit;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLeftLimit, xRightLimit),
        //                         Mathf.Clamp(transform.position.y, yDownLimit, yUpLimit));
        // = transform.position - target.transform.position;
        SwitchToNormalState(5);
    }
    void LateUpdate()
    {
        //LimitMovement();
        //Vector3 CamPos = target.transform.position + distanceToTarget;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, CamPos, cameraSpeed * Time.deltaTime);

        if (cameraState == CameraState.Normal)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, cameraSpeed);
            LimitMovement();
        }
    }
    void LimitMovement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xLeftLimit, xRightLimit),
                                 Mathf.Clamp(transform.position.y, yDownLimit, yUpLimit), transform.position.z);

    }
    
    public void SwitchToBossRoomState(Vector3 CamPos, float orthoSize, float tweenTime)
    {
        cameraState = CameraState.BossRoom;
        MainCamera.DOOrthoSize(orthoSize, tweenTime);
        transform.DOMove(CamPos, tweenTime);
    }

    public void SwitchToNormalState(float orthoSize)
    {
        cameraState = CameraState.Normal;
        MainCamera.orthographicSize = orthoSize;
    }
}

public enum CameraState
{
    Normal = 0,
    BossRoom = 1
}
