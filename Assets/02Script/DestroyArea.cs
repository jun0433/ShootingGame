using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    // 카메라 랜더 영역을 벗어나는 오브젝트를 소멸 시켜주는 역할. 
    private void OnTriggerExit2D(Collider2D collision)  // 겹쳐져 있던 오브젝트가 영역을 빠져나갈때 호출.
    {
        if (collision.CompareTag("Projectile")) // "Projectile" 태그를 가진 오브젝트와 작용
        {
            if (collision.TryGetComponent<ObjectPool_Label>(out ObjectPool_Label label))
                label.Push();
            else
                Destroy(collision.gameObject); // 해당 게임 오브젝트를 파괴. 

        }
    }


}
