using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerCont;

    private void Awake()
    {
        playerCont = GameObject.Find("Player").GetComponent<PlayerController>();

        playerCont.MOVEINPUT = true;
        playerCont.MOVEINPUT = false; 
    }

    private void OnMouseDown()
    {
        playerCont.MOVEINPUT = true;
    }

    private void OnMouseUp()
    {
        playerCont.MOVEINPUT = false;
    }
}
