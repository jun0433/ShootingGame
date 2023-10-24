using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type
{
    IT_Power,
    IT_Boom,
    IT_Heart,

}

public class FlyItem : ObjectPool_Label
{
    [SerializeField]
    private Item_Type type;
    private Movement2D movement;
    private int dirCounter;
    private Vector3 dir;
    private Vector3 moveTarget;

    private void Awake()
    {
        if(!TryGetComponent<Movement2D>(out movement))
        {
            Debug.Log("FlyItem.cs - Awake() - movement 참조 실패");
        }
    }

    public override void InitInfo()
    {
        base.InitInfo();
        dirCounter = 5;

        StartCoroutine("MoveCoroutine");

    }

    // 일정 주기별로 방향을 바꿔가면서 날아다님 (N회 반복 후 소멸)
    IEnumerator MoveCoroutine()
    {
        yield return null;

        while(dirCounter > 0)
        {
            dirCounter--;
            // 이동방향 전환
            SetMoveChange();
            yield return YieldInstructionCache.WaitForSeconds(4f);
        }

        Push();
    }

    private void SetMoveChange()
    {
        moveTarget.x = Random.Range(-2.5f, 2.5f);
        moveTarget.y = Random.Range(-3f, 0f);
        moveTarget.z = 0f;
        dir = moveTarget - transform.position;

        movement.InitMovement(dir.normalized); // 새로운 방향을 지정 (normalized를 하는 이유는 길이가 다른 vector가 생성되다 보면 버그가 생길 위험이 있기 때문에 최종 길이를 고정)
    }


    // Heart를 습득하면 회복을 진행
}
