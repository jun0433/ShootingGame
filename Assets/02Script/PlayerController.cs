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
                weapon.FIRING = value;  // 무기 작동상태 변경. 
            }

        }
    }

    private Vector3 pos;


    private void Awake()
    {
        
    }


    // Controller를 초기화하는 함수
    public void InitController()
    {
        isMove = false;
        if (!TryGetComponent<Weapon>(out weapon))
        {
            Debug.Log("PlayerController.cs - InitController() - weapon 참조 실패");
        }
        else
        {
            weapon.InitWeapon(projectile, attackRate);  // 무기 활성. 
        }

        if(!TryGetComponent<PlayerChar>(out playerChar))
        {
            Debug.Log("PlayerController.cs - InitController() - playerChar 참조 실패");
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
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 포지션을(스크린좌표계) 오브젝트의 포지션(월드좌표)로 변환

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
