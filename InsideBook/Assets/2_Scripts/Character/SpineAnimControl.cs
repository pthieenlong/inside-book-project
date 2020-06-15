using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAnimControl : MonoBehaviour
{
    public SkeletonAnimation Anim;
    public string currentState;

    public void SetAnimation(string animName, bool isLoop = true, float timeScale = 1)
    {
        if (currentState != animName)
        {
            Anim.state.SetAnimation(0 ,animName, isLoop).TimeScale = timeScale;
            currentState = animName;
        }
    }
}
