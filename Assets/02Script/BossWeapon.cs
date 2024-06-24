using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    AT_SingleFire,
    AT_5Shot,
    AT_CircleFire,
}

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private ObjectType projectileType;
    private float angle;
    private Vector2 dir;
    private float attackRate; // projectile �߻� ���� �ð��� �����ϴ� ����
    private GameObject obj;

    private AttackType curAttackType;

    public AttackType CurTYPE
    {
        set => curAttackType = value;
    }

    // ���� ����
    public void StartFireing(AttackType attackType)
    {
        StopFireing();
        curAttackType = attackType;
        StartCoroutine(attackType.ToString());
    }

    // ���� ����
    public void StopFireing()
    {
        StopAllCoroutines();

    }

    Vector3 targetPosition = Vector3.zero;

    IEnumerator AT_SingleFire()
    {
        attackRate = 0.1f;

        while (true)
        {
            obj = ObjectPool_Manager.Inst.pools[(int)projectileType].PopObj();
            obj.transform.position = transform.position;

            dir = targetPosition - transform.position;
            obj.GetComponent<Movement2D>().InitMovement(dir);

            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
        
    }

    IEnumerator AT_5Shot()
    {
        attackRate = 2f;
        int count = 5; // �߻��� projectile ���� ������ �����ϴ� ����
        float intervalAngle = 8f; 
        float weightAngle = 0f; // ������ ���� ���� �����ϴ� ����

        while (true)
        {
            // ���� ������ ���ݹ��� ����
            weightAngle = Random.Range(225f, 315f);

           for(int i = 0; i <  3; i++)
           {
                for(int j = 0; j < count; j++)
                {
                    obj = ObjectPool_Manager.Inst.pools[(int)projectileType].PopObj();
                    obj.transform.position = transform.position;

                    angle = weightAngle + j*5; // projectile ���� ���� ����

                    dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    obj.GetComponent<Movement2D>().InitMovement(dir);
                }
                yield return YieldInstructionCache.WaitForSeconds(0.1f);
           }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }


    }

    IEnumerator AT_CircleFire()
    {
        attackRate = 0.2f;
        int count = 30; // �ѹ߻��� projectile�� ������ �����ϴ� ����
        float intervalAngle = 360f/count; // projectile ������ ����
        float weightAngle = 0f;


        while (true)
        {
            for(int i = 0; i < count; i++)
            {
                obj = ObjectPool_Manager.Inst.pools[(int)projectileType].PopObj();
                obj.transform.position = transform.position;

                angle = weightAngle + intervalAngle * i;

                // ��׸� ������ �̿��ؼ� �̵� ���� dir�� ����
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);

                obj.GetComponent<Movement2D>().InitMovement(dir);
            }
            weightAngle += 2f;
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }
}
