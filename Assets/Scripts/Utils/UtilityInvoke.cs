using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityInvoke
{
    public static void Invoke(this MonoBehaviour mb , Action f , float delay)
    {

        mb.StartCoroutine(InvokeRoutine(f , delay));
    }

    private static IEnumerator InvokeRoutine(System.Action f , float delay)
    {
        yield return new WaitForSeconds(delay);
        f( );
    }

    public static float Remap(this float value , float from1 , float to1 , float from2 , float to2)
    {
        return ( value - from1 ) / ( to1 - from1 ) * ( to2 - from2 ) + from2;
    }
    public static float Remap2(this float value , float max)
    {
        return value/max;
    }

}
