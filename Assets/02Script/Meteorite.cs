using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : ObjectPool_Label
{
    private Rigidbody2D rig;
    private CircleCollider2D col;

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody2D>(out rig))
        {
            Debug.Log("Meteorite.cs - Awake() - rig 참조 실패");
        }
        else
        {
            rig.gravityScale = 2f;
        }

        if(!TryGetComponent<CircleCollider2D>(out col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }
    }

    // 플레이어에게 데미지 부여하는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerChar>(out PlayerChar player))
        {
            Debug.Log("플레이어와 충돌");
            player.TakeDamage(2);
            Push();
        }
    }
}
