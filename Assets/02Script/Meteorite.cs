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
            Debug.Log("Meteorite.cs - Awake() - rig ���� ����");
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

    // �÷��̾�� ������ �ο��ϴ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerChar>(out PlayerChar player))
        {
            Debug.Log("�÷��̾�� �浹");
            player.TakeDamage(2);
            Push();
        }
    }
}
