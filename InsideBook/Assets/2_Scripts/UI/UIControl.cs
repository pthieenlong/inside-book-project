using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIControl : MonoBehaviour
{
    public static UIControl Instance;
    public Image Fader;

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

    public void FadeOut()
    {
        Fader.DOFade(1, 0.5f);
        Fader.raycastTarget = true;
    }

    public void FadeIn()
    {
        Fader.DOFade(0, 0.5f);
        Fader.raycastTarget = false;
    }
}
