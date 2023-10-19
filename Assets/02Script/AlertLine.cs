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
            Debug.Log("AlertLine.cs - Awake() - anim ���� ����");
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

    // ���׿� ����
    private void SpawnMeteo()
    {

        Push();
    }
}
