using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType
{
    ObjT_Projectile_01, // 00
    ObjT_Projectile_02, // 01
    ObjT_Projectile_03, // 02
    ObjT_Projectile_04, // 03
    ObjT_Projectile_05, // 04
    ObjT_Projectile_06, // 05

    ObjT_Enemy_01, // 06
    ObjT_Enemy_02, // 07
    ObjT_Enemy_03, // 08

    ObjT_Effect_01, // 09
    ObjT_DropItem_01, // 10

    ObjT_AlertLine, // 11
    ObjT_Meteorite, // 12

    ObjT_PlayerBoom, // 13

    ObjT_FlyItemPower, // 14
    ObjT_FlyItemHeart, // 15
    ObjT_FlyItemboom, // 16
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
