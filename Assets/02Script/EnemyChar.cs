using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : ObjectPool_Label, IDamage
{
    [SerializeField]
    private int maxHP;

    private int curHP;

    private bool isAlive;

    private Movement2D movement;
    private GameObject obj;
    private SpriteRenderer sr;
    
   

    // 해당 오브젝트를 재활용하기 위해서 
    // 속성 값들을 초기화. == > 오브젝트 풀에서 꺼내질때. 
    public override void InitInfo()
    {
        base.InitInfo();
        curHP = maxHP; // 상대기체가 오브젝트 풀에서 꺼내져 나갈때는 항상. 풀HP 로 스폰되도록. 
        isAlive = true;

        if(!TryGetComponent<Movement2D>(out movement))
        {
            Debug.Log("EnemyChar.cs - InitInfo() - movement 참조 실패");
        }
        else
        {
            movement.InitMovement(Vector3.down);
        }

        if(!TryGetComponent<SpriteRenderer>(out sr))
        {
            Debug.Log("EnemyChar.cs - InitInfo() - sr 참조 실패");
        }
        else
        {
            sr.color = Color.white;
        }
        
    }

    
    private void OnHit()
    {
        curHP = curHP - Weapon.Inst.PLAYERDAMAGE;
        StartCoroutine("HitColor");
    }

    private IEnumerator HitColor()  // 비활성된 상태에서도 작동. 
    {
        // 빨간색으로 오브젝트를 변화
        sr.color = Color.red;
        // 일정시간 대기
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        // 다시 오브젝트를 흰색으로 복구
        if (gameObject.activeSelf)
            sr.color = Color.white;
    }

    // Enemy 사망시 호출 함수
    public void OnDie()
    {
        isAlive = false;
        GameManager.Inst.AddScore(5); // 몬스터 사망시 점수 추가
        GameManager.Inst.AddKillCount(false);

        obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Effect_01].PopObj();
        obj.transform.position = transform.position;

        SoundManager.Inst.PlaySFX(SFX_Type.SFX_Explosion_01);



        DropItem();
        Push(); // 오브젝트 풀에 반환
    }


    private int randCount;

    private void DropItem()
    {
        randCount = Random.Range(0, 100);

        if(randCount < 5)
        {
            // 파워 아이템 드랍
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemPower].PopObj();
            obj.transform.position = transform.position;
        }
        else if(randCount < 10)
        {
            // 회복 아이템 드랍
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemHeart].PopObj();
            obj.transform.position = transform.position;
        }
        else if(randCount < 15)
        {
            // 폭탄 아이템 드랍
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_FlyItemboom].PopObj();
            obj.transform.position = transform.position;

        }

        for (int i = 0; i < 7; i++)
        {
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_DropItem_01].PopObj();

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }


    }

    public void TakeDamge(int damage)
    {
        OnHit();
        if (curHP <= 0)
        {
            OnDie(); // 사망 처리
        }
    }
}
