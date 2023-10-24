using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : MonoBehaviour
{

    [SerializeField]
    private int curHP;

    [SerializeField]
    private int maxHP;

    private bool isAlive;

    public int CurHP
    {
        set
        {
            curHP = value;
        }
        get => curHP;
    }

    public int MaxHP => maxHP;

    // PlayerController에서 초기화
    public void InitChar()
    {
        CurHP = maxHP;
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            CurHP = curHP - damage;
            Debug.Log("플레이어 피격" + curHP);
            if(curHP <= 0)
            {
                isAlive = false;
                OnDie();
            }
        }
    }
    public void TakeHealing()
    {
        if(curHP < maxHP)
        {
            CurHP++;
        }
    }

    private void OnDie()
    {
        Debug.Log("플레이어 사망");
    }
}
