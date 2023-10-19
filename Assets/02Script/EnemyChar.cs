using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : ObjectPool_Label
{
    [SerializeField]
    private int maxHP;

    private int curHP;


    // �ش� ������Ʈ�� ��Ȱ���ϱ� ���ؼ� 
    // �Ӽ� ������ �ʱ�ȭ. == > ������Ʈ Ǯ���� ��������. 
    public override void InitInfo()
    {
        base.InitInfo();
        curHP = maxHP; // ����ü�� ������Ʈ Ǯ���� ������ �������� �׻�. ǮHP �� �����ǵ���. 
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && collision.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.Push(); // �ε��� ��� ����ü�� �Ҹ�. 

            curHP--;
            if (curHP <= 0)
                Push(); // �ڱ� �ڽŵ� ������Ʈ Ǯ�� ��ȯó��. 
        }
    }
}
