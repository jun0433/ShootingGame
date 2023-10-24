using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isInit = false;        // ���� ����ϱ� ���� ����
    private GameObject projectilePrefab;  // �������� ������ ������Ÿ��
    private float attackRate;   // ���� ����

    public void InitWeapon(GameObject projectile, float rate)
    {
        isInit = true;
        projectilePrefab = projectile;
        attackRate = rate;
    }

    private bool isFireing; // true �߻��� / false �߻� ����

    public bool FIRING
    {
        set
        {
            isFireing = value;

            if (isInit) // ���Ⱑ �ʱ�ȭ�� �ƴ��� Ȯ��
            {
                if (isFireing)
                {
                    // ���� �۵�.
                    StartCoroutine("TryAttack");
                }
                else
                {
                    // ���� �۵� ����. 
                    StopCoroutine("TryAttack");
                }
            }
        }

        get => isFireing;
    }

    private GameObject obj;
    private IEnumerator TryAttack()
    {
        while (true)
        {
            // �����ڵ� - �ǽð� ����.
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity); // ������Ÿ�� �߻�.

            // �����ڵ� - ������Ʈ Ǯ�� �̿��Ͽ� ������Ʈ�� ��Ȱ���ϴ� �ڵ�
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
            obj.transform.position = transform.position;

            yield return new WaitForSeconds(attackRate); // ���� ������. 
        }
    }


    [SerializeField]
    private int boomCount;

    public int BOOMCOUNT
    {
        get => boomCount;
        set
        {
            boomCount = value;
            GameManager.Inst.changeBoomText(boomCount);
        }
    }

    public void LunchBoom()
    {
        if(isInit && BOOMCOUNT > 0)
        {
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_PlayerBoom].PopObj();
            obj.transform.position = transform.position;
            BOOMCOUNT--;
        }
    }
}
