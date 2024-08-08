using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    BS_MoveToAppear = 0, // ���� ��, ���� ��ġ�� �̵�
    BS_Phase01,          // ���ڸ������ݺ����� ������Ÿ�� �߻�
    BS_Phase02,          // �¿�� �ݺ� �̵��ϸ鼭 ����
    BS_Stop,             // ���� ����� ���ڸ�
}

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState curState = BossState.BS_MoveToAppear;
    private Movement2D movement;
    private BossWeapon weapon;
    private BossChar myChar;


    public void InitBossAI(string name, int newHP)
    {
        if(!TryGetComponent<Movement2D>(out movement))
        {
            Debug.Log("Boss.CS - InitBossAI() - movement ���� ����");
        }
        if (!TryGetComponent<BossWeapon>(out weapon))
        {
            Debug.Log("Boss.CS - InitBossAI() - weapon ���� ����");
        }
        if (!TryGetComponent<BossChar>(out myChar))
        {
            Debug.Log("Boss.CS - InitBossAI() - myChar ���� ����");
        }
        else
        {
            myChar.InitBoss(name, newHP);
        }

        // ������ ���� ��ġ�� ����
        transform.position = new Vector3(0f, 7f, 0f);
        gameObject.SetActive(true);

        // myChar ������ �����Ͽ� HP�� ���� ȸ�� ��Ű��, �ʱ� �������� ����
        // ChnageState�� ���� AI ����

        // �����ϰ� ���� ��ġ�� �̵�
        ChangeState(BossState.BS_MoveToAppear);
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(curState.ToString()); // ���� ���� �ߴ�
        curState = newState;
        StartCoroutine(curState.ToString()); // ���ο� ���� ����
    }


    // ���� ��ġ�� �̵���Ű�� AI ����
    // �̵����� ����, ��ǥ ��ġ�� �����ϸ� Phase01 ����
    IEnumerator BS_MoveToAppear()
    {
        movement.InitMovement(Vector3.down);
        while (true)
        {
            if(transform.position.y <= bossAppearPointY)
            {
                movement.InitMovement(Vector3.zero); // �̵� �ߴ�
                ChangeState(BossState.BS_Phase01); // ���� ���� ����
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }

    private int randValue;

    IEnumerator BS_Phase01()
    {
        // ���� Ȱ��ȭ
        //weapon.StartFireing(AttackType.AT_5Shot);

        // HP�� �ۼ�Ʈ�� ����ġ ���Ϸ� ������ ��� 2������� ����
        while (true)
        {
            if((float)myChar.CURHP / (float)myChar.MAXHP > 0.5f)
            {
                randValue = Random.Range(0, 100);
                if(randValue < 50)
                {
                    weapon.StartFireing(AttackType.AT_5Shot);
                }
                else
                {
                    weapon.StartFireing(AttackType.AT_CircleFire);
                }
            }
            else if((float)myChar.CURHP / myChar.MAXHP <= 0.5f)
            {
                ChangeState(BossState.BS_Phase02);
            }
            yield return YieldInstructionCache.WaitForSeconds(3f); // N�ʿ� �� ���� HP�� ���� ���Ϸ� ���������� Ȯ��(�������� ����)
        }
    }

    private Vector2 moveDir = Vector2.right;
    // �翷���� �̵��ϸ鼭 �����ϴ� ����
    IEnumerator BS_Phase02()
    {
        moveDir = Vector2.right;
        movement.InitMovement(moveDir);

        while (true)
        {
            if(transform.position.x < -1.3f || transform.position.x > 1.3f)
            {
                moveDir *= -1f;
                movement.InitMovement(moveDir);
            }

            randValue = Random.Range(0, 100);
            if (randValue < 33)
            {
                weapon.StartFireing(AttackType.AT_5Shot);
            }
            else if(randValue < 66)
            {
                weapon.StartFireing(AttackType.AT_CircleFire);
            }
            else
            {
                weapon.StartFireing(AttackType.AT_SingleFire);
            }

            yield return YieldInstructionCache.WaitForSeconds(3f);
        }
    }

    IEnumerator BS_Stop()
    {
        movement.InitMovement(Vector3.zero);
        weapon.StopFireing();

        yield return YieldInstructionCache.WaitForSeconds(0.2f);
    }
}
