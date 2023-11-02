using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private Image img;


    private void Awake()
    {
        GameObject obj = GameObject.Find("FadeImg");
        if(obj != null)
        {
            if(obj.TryGetComponent<Image>(out img)){
                Debug.Log("FadeInOut.cs - Awake() - img ���� ����");
            }
        }
    }

    public void Fade_InOut(bool isIn, float fadeTime)
    {
        if (isIn)// fadeIn : ������� ��
        {
            StartCoroutine(Fade(1f, 0f, fadeTime));
        }
        else
        {
            StartCoroutine(Fade(0f, 1f, fadeTime));
        }
    }


    IEnumerator Fade(float start, float end, float fadeTime)
    {
       
        yield return null;
        fadeTime = Mathf.Clamp(fadeTime, 0.1f, 3f); // fadeTime ���� ����
        float curTime = 0f;
        float percent = 0f;

        Color alpha = img.color;

        while(percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / fadeTime;

            alpha.a = Mathf.Lerp(start, end, percent);
            img.color = alpha;
            yield return null;
        }

        if(end < 0.1f)
        {
            img.raycastTarget = false; // ��ư���� Ŭ�� ����
        }
        else
        {
            img.raycastTarget = true; // ��ư���� Ŭ�� �Ұ���
        }
    }
}
