using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : MonoBehaviour, IDamage
{

    [SerializeField]
    private int curHP;

    [SerializeField]
    private int maxHP;

    private bool isAlive;
    public bool ISALiVE => isAlive;

    public int CurHP
    {
        set
        {
            GameManager.Inst.ChangeHeart(curHP < value, value);

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

    public void TakeDamge(int damage)
    {
        if (isAlive)
        {
            CurHP = curHP - damage;
            // Debug.Log("플레이어 피격" + CurHP);
            if (curHP <= 0)
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
        GameManager.Inst.StageClear();
        // Debug.Log("플레이어 사망");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamge(1);
        }
    }
}
