using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : ObjectPool_Label
{
    [SerializeField]
    private int maxHP;

    private int curHP;


    // 해당 오브젝트를 재활용하기 위해서 
    // 속성 값들을 초기화. == > 오브젝트 풀에서 꺼내질때. 
    public override void InitInfo()
    {
        base.InitInfo();
        curHP = maxHP; // 상대기체가 오브젝트 풀에서 꺼내져 나갈때는 항상. 풀HP 로 스폰되도록. 
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && collision.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.Push(); // 부딪힌 상대 투사체를 소멸. 

            curHP--;
            if (curHP <= 0)
                Push(); // 자기 자신도 오브젝트 풀에 반환처리. 
        }
    }
}
