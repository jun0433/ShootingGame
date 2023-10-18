using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Label : MonoBehaviour
{
    protected ObjectPool pool; // 나를 관리해주는 관리자(pool)를 기억하기 위한 참조. 


    // 오브젝트 생성될때. 
    public virtual void Create(ObjectPool objectPool)
    {
        pool = objectPool; // 관리자 지정.
        gameObject.SetActive(false); //  비활성 상태로 관리. 
    }

    // 오브젝트 풀이 오브젝트를 꺼내갈때.
    public virtual void Pop()
    {
        Debug.Log("꺼내졌다.");
    }


    // 오브젝트 풀에 반환이 될때.
    public virtual void Push()
    {
        Debug.Log("반환처리");
        pool.PushObj(this);
    }


}
