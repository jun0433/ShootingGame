using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Label : MonoBehaviour
{
    protected ObjectPool pool; // ���� �������ִ� ������(pool)�� ����ϱ� ���� ����. 


    // ������Ʈ �����ɶ�. 
    public virtual void Create(ObjectPool objectPool)
    {
        pool = objectPool; // ������ ����.
        gameObject.SetActive(false); //  ��Ȱ�� ���·� ����. 
    }

    // ������Ʈ Ǯ�� ������Ʈ�� ��������.
    public virtual void Pop()
    {
        Debug.Log("��������.");
    }


    // ������Ʈ Ǯ�� ��ȯ�� �ɶ�.
    public virtual void Push()
    {
        Debug.Log("��ȯó��");
        pool.PushObj(this);
    }


}
