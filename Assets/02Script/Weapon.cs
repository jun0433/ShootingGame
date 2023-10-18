using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isInit = false;        // 무기 사용하기 위한 세팅이 끝났는지. 
    private GameObject projectilePrefab;  // 프로젝타일을 어떤 프리펩을 이용하여 생성할지.
    private float attackRate;   // 공격과 공격 사이에 시간. 

    public void InitWeapon(GameObject projectile, float rate)
    {
        isInit = true;
        projectilePrefab = projectile;
        attackRate = rate;
    }

    private bool isFireing; // true 프로젝타일 발사중,   false 발사하지 않는 상태. 

    public bool FIRING
    {
        set
        {
            isFireing = value;

            if (isInit) // 초기화가 된 무기들만 작동하도록. 
            {
                if (isFireing)
                {
                    // 무기 작동.
                    StartCoroutine("TryAttack");
                }
                else
                {
                    // 무기 작동 중지. 
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
            // 나쁜코드 - 실시간 생성.
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 프로젝타일 발사.

            // 좋은코드 - 오브젝트 풀을 이용하여 오브젝트를 재활용하는 코드
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
            obj.transform.position = transform.position;



            yield return new WaitForSeconds(attackRate); // 공격 딜레이. 
        }
    }

}
