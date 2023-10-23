using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLine : ObjectPool_Label
{

    private Animator anim;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("AlertLine.cs - Awake() - anim 참조 실패");
        }
    }

    public override void InitInfo()
    {
        base.InitInfo();
        anim.SetTrigger("Alert");
        Invoke("SpawnMeteo", 3.5f);

    }

    private Vector3 spawnPos;
    private GameObject spawnObj;

    // 메테오 생성
    private void SpawnMeteo()
    {
        spawnPos = transform.position;

        spawnPos.y = 6.0f;

        spawnObj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Meteorite].PopObj();
        spawnObj.transform.position = spawnPos;

        Push();
    }
}
