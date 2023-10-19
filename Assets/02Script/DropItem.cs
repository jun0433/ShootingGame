using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : ObjectPool_Label
{
    private Rigidbody2D rig;

    private bool isSetTarget; // 지정된 타켓이 흡수되고 있는 상태인지
    private GameObject moveTargetObj; // 타겟
    private float moveTime; // 타겟이 지정이 되고난 후 흐른 시간.

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody2D>(out rig))
        {
            Debug.Log("DropItem.cs - Awake() - rig 참조 실패");
        }
    }

    private Vector2 dropDir;

    public override void InitInfo()
    {
        base.InitInfo();
        dropDir.x = Random.Range(-1f, 1f);
        dropDir.y = Random.Range(3f, 4f);
        rig.velocity = dropDir;
        rig.gravityScale = 1f;
        isSetTarget = false;
    }

    public void SetTarget(GameObject newTarget)
    {
        moveTargetObj = newTarget;
        isSetTarget = true;

        rig.gravityScale = 0;
        rig.velocity = Vector2.zero;

        moveTime = 0f;
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
       
        if(isSetTarget && moveTargetObj.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, moveTargetObj.transform.position, moveTime / 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Push();
        }
    }
}
