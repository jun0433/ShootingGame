using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Weapon weapon;

    private PlayerChar playerChar;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float attackRate;


    private bool isMove;

    public bool MOVEINPUT
    {
        set
        {
            isMove = value;
            if(weapon != null)
            {
                weapon.FIRING = value;  // ���� �۵����� ����. 
            }

        }
    }

    private Vector3 pos;


    private void Awake()
    {
        
    }


    // Controller�� �ʱ�ȭ�ϴ� �Լ�
    public void InitController()
    {
        isMove = false;
        if (!TryGetComponent<Weapon>(out weapon))
        {
            Debug.Log("PlayerController.cs - InitController() - weapon ���� ����");
        }
        else
        {
            weapon.InitWeapon(projectile, attackRate);  // ���� Ȱ��. 
        }

        if(!TryGetComponent<PlayerChar>(out playerChar))
        {
            Debug.Log("PlayerController.cs - InitController() - playerChar ���� ����");
        }
        else
        {
            playerChar.InitChar();
        }
    }


    private void Update()
    {
        if (isMove)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��������(��ũ����ǥ��) ������Ʈ�� ������(������ǥ)�� ��ȯ

            pos.x = Mathf.Clamp(pos.x, -2.25f, 2.25f);
            pos.y = Mathf.Clamp(pos.y, -4.5f, 0f);
            pos.z = 0f;

            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.LunchBoom();
        }
    }

}
