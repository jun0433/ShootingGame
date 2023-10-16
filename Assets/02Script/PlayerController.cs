using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMove;

    public bool MOVEINPUT
    {
        set
        {
            isMove = value;
        }
    }

    private Vector3 pos;

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
    }

}
