using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType
{
    ObjT_Projectile_01,
    ObjT_Projectile_02,
    ObjT_Projectile_03,
    ObjT_Projectile_04,
    ObjT_Projectile_05,
    ObjT_Projectile_06,

    ObjT_Enemy_01,
    ObjT_Enemy_02,
    ObjT_Enemy_03,

    ObjT_Effect_01,
    ObjT_DropItem_01,

}


public class ObjectPool_Manager : MonoBehaviour
{
    private static ObjectPool_Manager instance;

    public static ObjectPool_Manager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
    }

    public List<ObjectPool> pools;
}
