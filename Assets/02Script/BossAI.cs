using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    BS_MoveToAppear = 0, // 스폰 후, 전투 위치로 이동
    BS_Phase01,          // 제자리에서반복적인 프로젝타일 발사
    BS_Phase02,          // 좌우로 반복 이동하면서 공격
    BS_Stop,             // 보스 사망시 제자리
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
            Debug.Log("Boss.CS - InitBossAI() - movement 참조 실패");
        }
        if (!TryGetComponent<BossWeapon>(out weapon))
        {
            Debug.Log("Boss.CS - InitBossAI() - weapon 참조 실패");
        }
        if (!TryGetComponent<BossChar>(out myChar))
        {
            Debug.Log("Boss.CS - InitBossAI() - myChar 참조 실패");
        }
        else
        {
            myChar.InitBoss(name, newHP);
        }

        // 보스를 스폰 위치로 변경
        transform.position = new Vector3(0f, 7f, 0f);
        gameObject.SetActive(true);

        // myChar 참조에 접근하여 HP를 전부 회복 시키고, 초기 설정으로 변경
        // ChnageState로 전투 AI 시작

        // 스폰하고 공격 위치로 이동
        ChangeState(BossState.BS_MoveToAppear);
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(curState.ToString()); // 현재 상태 중단
        curState = newState;
        StartCoroutine(curState.ToString()); // 새로운 상태 시작
    }


    // 전투 위치로 이동시키는 AI 상태
    // 이동방향 지정, 목표 위치로 도달하면 Phase01 시작
    IEnumerator BS_MoveToAppear()
    {
        movement.InitMovement(Vector3.down);
        while (true)
        {
            if(transform.position.y <= bossAppearPointY)
            {
                movement.InitMovement(Vector3.zero); // 이동 중단
                ChangeState(BossState.BS_Phase01); // 공격 패턴 시작
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }

    private int randValue;

    IEnumerator BS_Phase01()
    {
        // 무기 활성화
        //weapon.StartFireing(AttackType.AT_5Shot);

        // HP의 퍼센트가 기준치 이하로 떨어질 경우 2페이즈로 변경
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
            yield return YieldInstructionCache.WaitForSeconds(3f); // N초에 한 번씩 HP가 기준 이하로 떨어졌는지 확인(공격패턴 변경)
        }
    }

    private Vector2 moveDir = Vector2.right;
    // 양옆으로 이동하면서 공격하는 패턴
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
