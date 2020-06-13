using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FlyingMaskController : MonoBehaviour
{
    public SkeletonAnimation SAnim;
    public string currentState = MonsterState.Idle;
    public bool isJumping = false;
    void Start()
    {
        SetAnimation(MonsterState.Idle, true);
    }

    public void SetAnimation(string animName, bool isLoop = true, float timeScale = 1f){
        if (currentState != animName)
        {
            SAnim.state.SetAnimation(0, animName, isLoop).TimeScale = timeScale;
            currentState = animName;
        }
    }
}
public class MonsterState{
    public const string Idle = "animation";
    public const string Attack = "animation2";
    public const string Die = "animation3";
}