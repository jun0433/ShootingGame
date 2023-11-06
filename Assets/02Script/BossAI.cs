using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    BS_MoveToAppear = 0, // ���� ��, ���� ��ġ�� �̵�
    BS_Phase01,          // ���ڸ������ݺ����� ������Ÿ�� �߻�
    BS_Phase02,          // �¿�� �ݺ� �̵��ϸ鼭 ����
}

public class BossAI : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState curState = BossState.BS_MoveToAppear;
    private Movement2D movement;
    private BossWeapon weapon;
    private BossChar myChar;


    private void Awake()
    {
        InitBossAI("Boss1", 100);
    }
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

    IEnumerator BS_Phase01()
    {
        // ���� Ȱ��ȭ
        weapon.StartFireing(AttackType.AT_CircleFire);

        // 
        while (true)
        {
            yield return null;
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

            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }
}
