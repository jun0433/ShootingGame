using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField]
    private List<Transform> spawnTrans;
    [SerializeField]
    private float spawnDeltaTime;  // ���� �ð�. 

    private int waveCounter; // ���� ���̺긦 ��Ÿ���� ����. 
    private int spawnCounter; // �ش� ���̺꿡�� ����� ������ �̷�� ������ ī��Ʈ�� �����ϴ� ���� 

    [SerializeField]
    private List<GameObject> bossList;
    [SerializeField]
    private GameObject bossWarningText;
    [SerializeField]
    public GameObject bossHPUI;

    private int bossCounter;

    // Awake ��� ���
    public void InitSpawner()
    {
        waveCounter = 0;
        spawnCounter = 0;
        bossCounter = 0;
        Invoke("StartWave", 1f);
    }



    // ���̺� ���� �Լ�
    public void StartWave()
    {
        spawnCounter = 0;
        StartCoroutine("SpawnEvent");
    }

    // ���̺� �ߴ� �Լ�
    public void StopWave()
    {
        StopCoroutine("SpawnEvent");
    }


    private GameObject obj;

    IEnumerator SpawnEvent() // �ݺ������� ���͸� ����. 
    {
        while (true)
        {
            spawnCounter++;
            for (int i = 0; i < spawnTrans.Count; i++)
            {
                obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Enemy_01+waveCounter].PopObj();
                obj.transform.position = spawnTrans[i].position;
            }

            // ���� ������ ���̺� ����
            if (spawnCounter >= 10)
            {
                Debug.Log("���� ����");
                StopCoroutine("SpawnEvent");
                StartCoroutine("SpawnBoss");
            }

            yield return YieldInstructionCache.WaitForSeconds(spawnDeltaTime);  // ������ ���� ������ ������ �ο�. 
        }
    }

    private void GameClear()
    {
        GameManager.Inst.StageClear();
    }


    // ������ �����ϴ� �Լ�(���� ü�� �� ���� ����)
    private IEnumerator SpawnBoss()
    {
        waveCounter++;

        // ���� ���� ���� ǥ��
        bossWarningText.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(5f);
        bossWarningText.SetActive(false);

        // ������� ���� ��������� ����
        SoundManager.Inst.ChangeBGM(BGM_Type.BGM_Boss);
        
        // ������ ������� ����(ü���� ���� �� ���� ����)
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
