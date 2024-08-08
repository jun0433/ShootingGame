using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
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
    public GameObject bossHPUI;

    private int bossCounter;

    // Awake 대신 사용
    public void InitSpawner()
    {
        waveCounter = 0;
        spawnCounter = 0;
        bossCounter = 0;
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
                obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Enemy_01+waveCounter].PopObj();
                obj.transform.position = spawnTrans[i].position;
            }

            // 보스 전까지 웨이브 지정
            if (spawnCounter >= 10)
            {
                Debug.Log("보스 스폰");
                StopCoroutine("SpawnEvent");
                StartCoroutine("SpawnBoss");
            }

            yield return YieldInstructionCache.WaitForSeconds(spawnDeltaTime);  // 스폰과 스폰 사이의 딜레이 부여. 
        }
    }

    private void GameClear()
    {
        GameManager.Inst.StageClear();
    }


    // 보스를 스폰하는 함수(보스 체력 및 보스 설정)
    private IEnumerator SpawnBoss()
    {
        waveCounter++;

        // 보스 출현 문구 표시
        bossWarningText.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(5f);
        bossWarningText.SetActive(false);

        // 배경음을 보스 배경음으로 변경
        SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Boss);
        
        // 보스를 순서대로 스폰(체력은 스폰 시 마다 증가)
        switch (bossCounter%3)
        {
            case 0:
                bossList[0].GetComponent<BossAI>().InitBossAI("Boss" + (bossCounter + 1), 10 * (bossCounter + 1));
                break;
            case 1:
                bossList[1].GetComponent<BossAI>().InitBossAI("Boss" + (bossCounter + 1), 10 * (bossCounter + 1));
                break;
            case 2:
                bossList[2].GetComponent<BossAI>().InitBossAI("Boss" + (bossCounter + 1), 10 * (bossCounter + 1));
                break;

        }
        bossCounter++;
        bossHPUI.SetActive(true);
    }

}
