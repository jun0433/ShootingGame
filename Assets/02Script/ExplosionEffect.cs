using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : ObjectPool_Label
{
    private ParticleSystem ps;
    private void Awake()
    {
        if (!TryGetComponent<ParticleSystem>(out ps))
        {
            Debug.Log("ExplosionEffect.cs - Awake() - ps 참조 실패");
        }
    }

    public override void InitInfo()
    {
        base.InitInfo();
        ps.Play();
    }


    private void Update()
    {
        if (ps.isPlaying == false)
        {
            Push();
        }
    }
}
