using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class LilyAnimControl : MonoBehaviour
{
    public SkeletonAnimation SAnim;
    string currentState = LilyState.Idle;
    float baseDirection = 1;

    void Start()
    {
        baseDirection = SAnim.transform.localScale.x;
    }
    public void SetAnimation(string animName, bool isLoop = true, float timeScale = 1)
    {
        if (currentState != animName)
        {
            SAnim.state.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
            currentState = animName;
        }
    }

    public void SetDirection(bool isLeft)
    {
        SAnim.transform.localScale = SAnim.transform.localScale.V3SetX(baseDirection * (isLeft ? -1 : 1));
    }
}

public class LilyState
{
    public const string Idle = "tho";
    public const string Idle_Empty_Hand = "tho_ban dau";
    public const string Move = "dichuyen_cam";
    public const string Move_Empty_Hand = "di chuyen2";
}
