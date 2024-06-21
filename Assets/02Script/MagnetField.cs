using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField : MonoBehaviour
{
    private CircleCollider2D col;

    private void Awake()
    {
        if(!TryGetComponent<CircleCollider2D>(out col))
        {
            Debug.Log("MagnetField.cs - Awake() - col 참조 실패");
        }
        else
        {
            col.isTrigger = true;
            col.radius = 1f;
        }
    }



    // 아이템과 부딪히면 아이템을 획득하는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item") && collision.TryGetComponent<DropItem>(out DropItem item))
        {
            item.SetTarget(transform.parent.gameObject);
        }
    }
}
