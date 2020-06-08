using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class LilyAnimControl : MonoBehaviour
{
    public SkeletonAnimation SAnim;
    public string currentState = LilyState.Idle;
    public bool isJumping = false;

    void Start()
    {
        // baseDirection = SAnim.transform.localScale.x;
        SetAnimation(LilyState.Idle, true);
    }
    public void SetAnimation(string animName, bool isLoop = true, float timeScale = 1)
    {
        if (currentState != animName)
        {
            SAnim.state.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
            currentState = animName;
        }
    }
}

public class LilyState
{
    public const string Idle = "tho";
    public const string Idle_Empty_Hand = "tho_ ban dau";
    public const string Move = "dichuyen_cam";
    public const string Move_Empty_Hand = "di chuyen2";
    public const string Jump_Start = "animation - nhay rot tu tren cao xuong frame 1";
    public const string Jumping = "animation-nhay roi tu treo cao xuong fame 2";
    public const string Falling = "animation -nhay roi tu tren cao xuong fame 3";
    public const string Dead = "hit3";
}
