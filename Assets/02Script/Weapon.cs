using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isInit = false;        // 무기 사용하기 위한 세팅
    private GameObject projectilePrefab;  // 프리펩을 선택할 프로젝타일
    private float attackRate;   // 공격 간격

    public void InitWeapon(GameObject projectile, float rate)
    {
        isInit = true;
        projectilePrefab = projectile;
        attackRate = rate;
    }

    private bool isFireing; // true 발사중 / false 발사 중지

    public bool FIRING
    {
        set
        {
            isFireing = value;

            if (isInit) // 무기가 초기화가 됐는지 확인
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
