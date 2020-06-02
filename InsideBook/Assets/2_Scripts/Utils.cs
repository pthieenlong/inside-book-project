using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    #region Vector Extensions
    public static void V3SetX(this Vector3 v, float x)
    {
        Vector3 tempV = v;
        tempV.x = x;
        v = tempV;
    }
    public static void V3SetY(this Vector3 v, float y)
    {
        Vector3 tempV = v;
        tempV.y = y;
        v = tempV;
    }

    public static Vector2 V2SetX(this Vector2 v, float x)
    {
        Vector2 tempV = v;
        tempV.x = x;
        v = tempV;
        return v;
    }
    public static Vector2 V2SetY(this Vector2 v, float y)
    {
        Vector2 tempV = v;
        tempV.y = y;
        v = tempV;
        return v;
    }
    #endregion Vector Extensions
}
