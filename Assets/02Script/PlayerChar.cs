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

    public int CuHP
    {
        set
        {
            curHP = value;
        }
        get => curHP;
    }

    public int MaxHP => maxHP;

    // PlayerController���� �ʱ�ȭ
    public void InitChar()
    {
        curHP = maxHP;
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            curHP = CuHP - damage;
            Debug.Log("�÷��̾� �ǰ�" + curHP);
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
            CuHP++;
        }
    }

    private void OnDie()
    {
        Debug.Log("�÷��̾� ���");
    }
}
