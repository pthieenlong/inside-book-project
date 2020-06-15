using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    public float duration;

    void OnEnable()
    {
        Invoke("Deactive", duration);
    }
    public void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
