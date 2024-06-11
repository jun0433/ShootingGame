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
    private bool isInit = false;        // ���� ����ϱ� ���� ����
    private GameObject projectilePrefab;  // �������� ������ ������Ÿ��
    private float attackRate;   // ���� ����
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
            // �ǽð� ����.
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity); // ������Ÿ�� �߻�.

            // ������Ʈ Ǯ�� �̿��Ͽ� ������Ʈ�� ��Ȱ���ϴ� �ڵ�
            switch (curState)
            {
                case FireType.FT_Single: // �� �� ����
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position; // ��ġ
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up); // ����
                    break;
                case FireType.FT_Double: // �� �� ����
                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(-0.1f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);

                    obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
                    obj.transform.position = transform.position + new Vector3(0.1f, 0f, 0f);
                    obj.GetComponent<Movement2D>().InitMovement(Vector3.up);
                    break;
                case FireType.FT_Triple: // �� �� ����
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

            SoundManager.Inst.PlaySFX(SFX_Type.SFX_Fire_01); // ���� ���� ���

            yield return new WaitForSeconds(attackRate); // ���� ������. 
        }
    }

    // �÷��̾� ������
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
