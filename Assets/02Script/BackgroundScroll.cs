using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 3f;

    [SerializeField]
    private Vector3 startPos;

    private void Update()
    {
        transform.position += scrollSpeed * Time.deltaTime * Vector3.down;

        if(transform.position.y <= -12.75f)
        {
            transform.position = startPos;
        }
    }
}
