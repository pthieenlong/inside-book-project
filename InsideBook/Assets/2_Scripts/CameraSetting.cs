using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public GameObject target;

    public float cameraSpeed = 0.125f;
    public Vector3 offset;
    public float xRightLimit;
    public float xLeftLimit;
    public float yUpLimit, yDownLimit;
    private Vector3 Limit;
    void Start(){
        Limit = new Vector3(Mathf.Clamp(transform.position.x, xLeftLimit, xRightLimit),
                                 Mathf.Clamp(transform.position.y, yDownLimit, yUpLimit));
        // = transform.position - target.transform.position;
    }
    void LateUpdate(){
        
        //Vector3 CamPos = target.transform.position + distanceToTarget;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, CamPos, cameraSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, cameraSpeed); 
        //transform.position = Vector3.Lerp(Limit, target.transform.position + offset,cameraSpeed);
        
    }
}
