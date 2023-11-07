using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 인터페이스 (추상클래스 - 순수가상함수)
interface IDamage
{
    void TakeDamge(int damage);
}

public class Projectile : ObjectPool_Label
{
    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if(collision.TryGetComponent<IDamage>(out IDamage damage))
            {
                damage.TakeDamge(1);
            }
        }
    }
}
