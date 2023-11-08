using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossWarning : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;

    private TextMeshProUGUI textBoss;

    private void Awake()
    {
        textBoss = GetComponent<TextMeshProUGUI>();
    }


    // 해당 오브젝트가 활성화 되는 시점에 호출
    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    // 해당 오브게트가 비활성화 되는 시점에 호출
    private void OnDisable()
    {
        
    }

    private IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerpLoop(Color.white, Color.red));
            yield return StartCoroutine(ColorLerpLoop(Color.red, Color.white));
        }
    } 

    private IEnumerator ColorLerpLoop(Color start, Color end)
    {
        float currentTime = 0f;
        float percent = 0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;
            textBoss.color = Color.Lerp(start, end, percent);
            yield return null;
        }
    }
}
