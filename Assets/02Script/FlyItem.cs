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
            Debug.Log("FlyItem.cs - Awake() - movement ���� ����");
        }
    }

    public override void InitInfo()
    {
        base.InitInfo();
        dirCounter = 5;

        StartCoroutine("MoveCoroutine");

    }

    // ���� �ֱ⺰�� ������ �ٲ㰡�鼭 ���ƴٴ� (Nȸ �ݺ� �� �Ҹ�)
    IEnumerator MoveCoroutine()
    {
        yield return null;

        while(dirCounter > 0)
        {
            dirCounter--;
            // �̵����� ��ȯ
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

        movement.InitMovement(dir.normalized); // ���ο� ������ ���� (normalized�� �ϴ� ������ ���̰� �ٸ� vector�� �����Ǵ� ���� ���װ� ���� ������ �ֱ� ������ ���� ���̸� ����)
    }


    // Heart�� �����ϸ� ȸ���� ����
}
