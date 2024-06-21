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
            Debug.Log("MagnetField.cs - Awake() - col ���� ����");
        }
        else
        {
            col.isTrigger = true;
            col.radius = 1f;
        }
    }



    // �����۰� �ε����� �������� ȹ���ϴ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item") && collision.TryGetComponent<DropItem>(out DropItem item))
        {
            item.SetTarget(transform.parent.gameObject);
        }
    }
}
