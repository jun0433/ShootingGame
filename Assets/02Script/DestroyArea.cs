using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    // ī�޶� ���� ������ ����� ������Ʈ�� �Ҹ� �����ִ� ����. 
    private void OnTriggerExit2D(Collider2D collision)  // ������ �ִ� ������Ʈ�� ������ ���������� ȣ�� �̺�Ʈ.
    {
        if (collision.CompareTag("Projectile"))
        {
            //Destroy(collision.gameObject); // �ش� ���� ������Ʈ�� �ı�. 
            if (collision.TryGetComponent<ObjectPool_Label>(out ObjectPool_Label label))
                label.Push();
            else
                Destroy(collision.gameObject); // �ش� ���� ������Ʈ�� �ı�. 

        }
    }


}
