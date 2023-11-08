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


    // �ش� ������Ʈ�� Ȱ��ȭ �Ǵ� ������ ȣ��
    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    // �ش� �����Ʈ�� ��Ȱ��ȭ �Ǵ� ������ ȣ��
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
