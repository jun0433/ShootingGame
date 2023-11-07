using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnTrans;
    [SerializeField]
    private float spawnDeltaTime;  // 스폰 시간. 

    private int waveCounter; // 현재 웨이브를 나타내는 변수. 
    private int spawnCounter; // 해당 웨이브에서 몇번의 스폰이 이루어 졌는지 카운트를 관리하는 변수 

    [SerializeField]
    private List<GameObject> bossList;
    [SerializeField]
    private GameObject bossWarningText;
    [SerializeField]
    private GameObject bossHPUI;

    // Awake 대신 사용
    public void InitSpawner()
    {
        waveCounter = 0;
        spawnCounter = 0;
        Invoke("StartWave", 1f);
    }



    // 웨이브 시작 함수
    public void StartWave()
    {
        spawnCounter = 0;
        StartCoroutine("SpawnEvent");
    }

    // 웨이브 중단 함수
    public void StopWave()
    {
        StopCoroutine("SpawnEvent");
    }


    private GameObject obj;

    IEnumerator SpawnEvent() // 반복적으로 몬스터를 스폰. 
    {
        while (true)
        {
            spawnCounter++;
            for (int i = 0; i < spawnTrans.Count; i++)
            {
                obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Enemy_01].PopObj();
                obj.transform.position = spawnTrans[i].position;
            }

            if (spawnCounter >= 10)
            {
                Debug.Log("보스 스폰");
                StopCoroutine("SpawnEvent");
                StartCoroutine("SpawnBoss");
            }

            yield return YieldInstructionCache.WaitForSeconds(spawnDeltaTime);  // 스폰과 스폰 사이의 딜레이 부여. 
        }
    }

    private IEnumerator SpawnBoss()
    {
        bossWarningText.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(5f);
        bossWarningText.SetActive(false);
        SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Boss);
        bossList[0].GetComponent<BossAI>().InitBossAI("Boss2", 500);
        bossHPUI.SetActive(true);
    }
}
