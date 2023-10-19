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

        if (!waitForSeconds.TryGetValue(second, out wfs))  // ������ ��ġ�ϴ� Ű���� �ִٸ�, �ش� value�� ����, 
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second)); // ��ġ�ϴ� Ű���� �������� ���Ӱ� ��ü ����. 
        }
        return wfs;
    }

}
