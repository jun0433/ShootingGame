using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �������̽� (�߻�Ŭ���� - ���������Լ�)
interface IDamage
{
    void TakeDamge(int damage);
}

public class Projectile : ObjectPool_Label
{

    // Projectile�� ��ȣ�ۿ� �� Tag�� ����
    [SerializeField]
    private string targetTag;

    [SerializeField]
    private string targetTag2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ Tag�� ��ȣ�ۿ� �ǰ� ����
        if (collision.CompareTag(targetTag)||collision.CompareTag(targetTag2))
        {
            if(collision.TryGetComponent<IDamage>(out IDamage damage))
            {
                damage.TakeDamge(1);
            }
        }

    }


}
