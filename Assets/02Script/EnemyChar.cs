using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : ObjectPool_Label, IDamage
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

    
    private void OnHit()
    {
        curHP = curHP - Weapon.Inst.PLAYERDAMAGE;
        StartCoroutine("HitColor");
    }

    private IEnumerator HitColor()  // ��Ȱ���� ���¿����� �۵�. 
    {
        // ���������� ������Ʈ�� ��ȭ
        sr.color = Color.red;
        // �����ð� ���
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        // �ٽ� ������Ʈ�� ������� ����
        if (gameObject.activeSelf)
            sr.color = Color.white;
    }

    // Enemy ����� ȣ�� �Լ�
    public void OnDie()
    {
        isAlive = false;
        GameManager.Inst.AddScore(5); // ���� ����� ���� �߰�
        GameManager.Inst.AddKillCount(false);

        obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Effect_01].PopObj();
        obj.transform.position = transform.position;

        SoundManager.Inst.PlaySFX(SFX_Type.SFX_Explosion_01);



        DropItem();
        Push(); // ������Ʈ Ǯ�� ��ȯ
    }


    private int randCount;

    private void DropItem()
    {
        randCount = Random.Range(0, 100);

        if(randCount < 5)
        {
            // �Ŀ� ������ ���
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemPower].PopObj();
            obj.transform.position = transform.position;
        }
        else if(randCount < 10)
        {
            // ȸ�� ������ ���
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemHeart].PopObj();
            obj.transform.position = transform.position;
        }
        else if(randCount < 15)
        {
            // ��ź ������ ���
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemboom].PopObj();
            obj.transform.position = transform.position;

        }

        for (int i = 0; i < 7; i++)
        {
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_DropItem_01].PopObj();

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }


    }

    public void TakeDamge(int damage)
    {
        OnHit();
        if (curHP <= 0)
        {
            OnDie(); // ��� ó��
        }
    }
}
