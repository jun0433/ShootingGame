using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnTrans;
    [SerializeField]
    private float spawnDeltaTime;  // ���� �ð�. 

    private int waveCounter; // ���� ���̺긦 ��Ÿ���� ����. 
    private int spawnCounter; // �ش� ���̺꿡�� ����� ������ �̷�� ������ ī��Ʈ�� �����ϴ� ���� 



    // Awake ��� ���
    public void InitSpawner()
    {
        waveCounter = 0;
        spawnCounter = 0;
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
                obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Enemy_01].PopObj();
                obj.transform.position = spawnTrans[i].position;
            }

            if (spawnCounter >= 10)
            {
                Debug.Log("���� ����");
            }

            yield return YieldInstructionCache.WaitForSeconds(spawnDeltaTime);  // ������ ���� ������ ������ �ο�. 
        }
    }

}
