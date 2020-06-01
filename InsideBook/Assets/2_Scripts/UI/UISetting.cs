using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
    }
}
