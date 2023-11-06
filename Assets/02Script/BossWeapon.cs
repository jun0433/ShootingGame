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
    private float attackRate;
    private GameObject obj;

    private AttackType curAttackType;

    // 공격 시작
    public void StartFireing(AttackType attackType)
    {
        StopFireing();
        curAttackType = attackType;
        StartCoroutine(attackType.ToString());
    }

    // 공격 멈춤
    public void StopFireing()
    {
        StopAllCoroutines();

    }

    IEnumerator AT_SingleFire()
    {
        yield return null;
    }

    IEnumerator AT_5Shot()
    {
        yield return null;
    }

    IEnumerator AT_CircleFire()
    {
        attackRate = 0.7f;
        int count = 30;
        float intervalAngle = 360f/count;
        float weightAngle = 0f;


        while (true)
        {
            for(int i = 0; i < count; i++)
            {
                obj = ObjectPool_Manager.Inst.pools[(int)projectileType].PopObj();
                obj.transform.position = transform.position;

                angle = weightAngle + intervalAngle * i;

                // 디그리 각도를 이용해서 이동 방향 dir을 생성
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);

                obj.GetComponent<Movement2D>().InitMovement(dir);
            }
            weightAngle += 2f;
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }
}
