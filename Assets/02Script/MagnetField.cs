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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item") && collision.TryGetComponent<DropItem>(out DropItem item))
        {
            Debug.Log("DropItem 습득 프로세스 시작");
            item.SetTarget(transform.parent.gameObject);
        }
    }
}
