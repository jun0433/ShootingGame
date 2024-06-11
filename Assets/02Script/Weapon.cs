using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
    FT_Single,
    FT_Double,
    FT_Triple,
    FT_UpgradeSing,
    FT_UpgradeDouble,
}

public class Weapon : Singleton<Weapon>
{
    private bool isInit = false;        // 무기 사용하기 위한 세팅
    private GameObject projectilePrefab;  // 프리펩을 선택할 프로젝타일
    private float attackRate;   // 공격 간격
    private FireType curState;
    private int value;


    public void InitWeapon(GameObject projectile, float rate)
    {
        isInit = true;
        projectilePrefab = projectile;
        attackRate = rate;

        if(PlayerPrefs.GetInt(Skill_Type.ST_Skill_05.ToString()) == 1)
        {
            attackRate = 0.03f;
        }
        else if (PlayerPrefs.GetInt(Skill_Type.ST_Skill_03.ToString()) == 1)
        {
            attackRate = 0.05f;
        }
        else if (PlayerPrefs.GetInt(Skill_Type.ST_Skill_01.ToString()) == 1)
        {
            attackRate = 0.07f;
        }

        if (PlayerPrefs.GetInt(Skill_Type.ST_Skill_04.ToString()) == 1)
        {
            curState = FireType.FT_Triple;
        }
        else if (PlayerPrefs.GetInt(Skill_Type.ST_Skill_02.ToString()) == 1)
        {
            curState = FireType.FT_Double;
        }
        else
        {
            curState = FireType.FT_Single;
        }

        if(PlayerPrefs.GetInt(Skill_Type.ST_Skill_06.ToString()) == 1)
        {
            boomCount = 10;
        }
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
            // 실시간 생성.
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity); // 프로젝타일 발사.

            // 오브젝트 풀을 이용하여 오브젝트를 재활용하는 코드
            switch (curState)
            {
                case FireType.FT_Single: // 한 발 생성
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position; // 위치
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up); // 방향
                    break;
                case FireType.FT_Double: // 두 발 생성
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(-0.1f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);

                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(0.1f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);
                    break;
                case FireType.FT_Triple: // 세 발 생성
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position;
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);

                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(-0.2f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);

                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(0.2f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);
                    break;
            }

            SoundManager.Inst.PlaySFX(SFX_Type.SFX_Fire_01); // 공격 사운드 재생

            yield return new WaitForSeconds(attackRate); // 공격 딜레이. 
        }
    }

    // 플레이어 데미지
    private int playerDamage = 1;
    public int PLAYERDAMAGE
    {
        get => playerDamage;
        set => playerDamage = value;
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
