using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystick : MonoBehaviour
{
    public static UIJoystick Instance;
    public static Vector3 JoystickDirection;
    public Transform origin;
    public Transform handle;
    public float LimitRange;
    Vector3 VectorClamp;
    void Awake()
    {
        Instance = this;
    }
    public void OnPointerDown()
    {
        if (Input.mousePosition != null)
        {
            VectorClamp = Input.mousePosition;
            VectorClamp.x = Mathf.Clamp(VectorClamp.x, origin.position.x - LimitRange, origin.position.x + LimitRange);
            VectorClamp.y = Mathf.Clamp(VectorClamp.y, origin.position.y - LimitRange, origin.position.y + LimitRange);

            handle.position = VectorClamp;
            SetDirection();
        }
    }

    public void OnPointerUp()
    {
        handle.position = origin.position;
        JoystickDirection = Vector3.zero;
    }

    public void MoveLeft()
    {
        VectorClamp = handle.position;
        VectorClamp.x = origin.position.x - LimitRange;
        handle.position = VectorClamp;
        SetDirection();
    }

    public void MoveRight()
    {
        VectorClamp = handle.position;
        VectorClamp.x = origin.position.x + LimitRange;
        handle.position = VectorClamp;
        SetDirection();
    }

    public void MoveUp()
    {
        VectorClamp = handle.position;
        VectorClamp.y = origin.position.y - LimitRange;
        handle.position = VectorClamp;
        SetDirection();
    }

    public void MoveDown()
    {
        VectorClamp = handle.position;
        VectorClamp.y = origin.position.y + LimitRange;
        handle.position = VectorClamp;
        SetDirection();
    }

    void SetDirection()
    {
        JoystickDirection = (handle.position - origin.position);
        JoystickDirection.Normalize();
    }
}
