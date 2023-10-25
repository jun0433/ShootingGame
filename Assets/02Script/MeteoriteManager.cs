using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    [SerializeField]
    private float meteoSpawnRate;


    private GameObject obj;
    private Vector3 spawnPos;


    public void InitMeteorite()
    {
        StartCoroutine("SpawnAlertLine");
    }

    IEnumerator SpawnAlertLine()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(meteoSpawnRate);
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_AlertLine].PopObj();

            spawnPos.x = Random.Range(-2.5f, 2.5f);
            spawnPos.y = 0f;
            spawnPos.z = 0f;
            obj.transform.position = spawnPos;
        }
        
    }
}
