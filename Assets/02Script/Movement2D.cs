using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0f;

    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    // 이동 방향
    public void InitMovement(Vector3 dir)
    {
        moveDirection = dir;
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDirection;
    }
}
