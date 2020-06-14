using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGhostAnimControl : SpineAnimControl
{
    void Start()
    {
        SetAnimation(TheGhostState.Idle);
    }
}

public class TheGhostState
{
    public const string Idle = "hinh thai ban dau";
    public const string Raise = "xuat hien";
    public const string Attack1 = "skill_danh thuong";
    public const string Attack2 = "skill_luot chem";
    public const string Attack3 = "skill_uti";
    public const string Die = "die";
    public const string GetHit = "bi danh";
}
