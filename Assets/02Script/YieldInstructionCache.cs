using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class YieldInstructionCache
{
    //public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    // MAP 
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitForSeconds(float second)   // 0.01 
    {
        WaitForSeconds wfs;

        if (!waitForSeconds.TryGetValue(second, out wfs))  // 사전에 일치하는 키값이 있다면, 해당 value를 리턴, 
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second)); // 일치하는 키값이 없을때만 새롭게 객체 생성. 
        }
        return wfs;
    }

}
