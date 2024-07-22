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

    // Projectile에 상호작용 할 Tag를 설정
    [SerializeField]
    private string targetTag;

    [SerializeField]
    private string targetTag2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 정해진 Tag에 상호작용 되게 설정
        if (collision.CompareTag(targetTag)||collision.CompareTag(targetTag2))
        {
            if(collision.TryGetComponent<IDamage>(out IDamage damage))
            {
                damage.TakeDamge(1);
            }
        }

    }


}
