using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : ObjectPool_Label
{
    [SerializeField]
    private int maxHP;

    private int curHP;

    private bool isAlive;

    private Movement2D movement;
    private GameObject obj;
    private SpriteRenderer sr;


    // �ش� ������Ʈ�� ��Ȱ���ϱ� ���ؼ� 
    // �Ӽ� ������ �ʱ�ȭ. == > ������Ʈ Ǯ���� ��������. 
    public override void InitInfo()
    {
        base.InitInfo();
        curHP = maxHP; // ����ü�� ������Ʈ Ǯ���� ������ �������� �׻�. ǮHP �� �����ǵ���. 
        isAlive = true;

        if(!TryGetComponent<Movement2D>(out movement))
        {
            Debug.Log("EnemyChar.cs - InitInfo() - movement ���� ����");
        }
        else
        {
            movement.InitMovement(Vector3.down);
        }

        if(!TryGetComponent<SpriteRenderer>(out sr))
        {
            Debug.Log("EnemyChar.cs - InitInfo() - sr ���� ����");
        }
        else
        {
            sr.color = Color.white;
        }
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && collision.TryGetComponent<Projectile>(out Projectile projectile) && isAlive) // ������� ���� ������ ó��.
        {
            projectile.Push(); // �ε��� ��� ����ü�� �Ҹ�. 

            OnHit();
            if (curHP <= 0)
                OnDie(); // ��� ó��
        }
    }
    
    private void OnHit()
    {
        curHP--;
        Debug.Log("ü�� : " + curHP);
        // StartCoroutine("HitColor");
    }

    // Enemy ����� ȣ�� �Լ�
    private void OnDie()
    {
        isAlive = false;
        Push(); // ������Ʈ Ǯ�� ��ȯ

        obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Effect_01].PopObj();
        obj.transform.position = transform.position;

        DropItem();
    }


    private void DropItem()
    {
        for(int i = 0; i < 7; i++)
        {
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_DropItem_01].PopObj();

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }

    }
}
