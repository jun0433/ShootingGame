using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerCont;

    private void Awake()
    {
        playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // 마우스의 움직임을 처리하는 함수
    private void OnMouseDown()
    {
        if(GameManager.Inst.CurState == GameState.GS_Play && playerCont != null)
        {
            playerCont.MOVEINPUT = true;
        }
    }

    private void OnMouseUp()
    {
        if(playerCont != null)
        {
            playerCont.MOVEINPUT = false;
        }

    }
}
