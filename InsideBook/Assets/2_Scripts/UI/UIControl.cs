using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public static UIControl Instance;
    void Awake()
    {
        Instance = this;
    }
    public void ShowUI()
    {
        this.gameObject.SetActive(true);
    }

    public void HideUi()
    {
        this.gameObject.SetActive(false);
    }
}
