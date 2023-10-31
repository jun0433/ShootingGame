using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public enum BGM_Type
{
    BGM_Noraml = 0,
    BGM_Boss,
}

public enum SFX_Type
{
    SFX_Fire_01,
    SFX_Explosion_01,
}

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    AudioSource bgmSource;


    private static SoundManager inst;

    public static SoundManager Inst
    {
        get
        {
            return inst;
        }
    }

    private void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            inst = this;
        }
    }

    [SerializeField]
    public List<AudioClip> sfx_list;

    private int cursor = 0;
    public void PlaySFX(SFX_Type sfx)
    {
        audioSources[cursor].clip = sfx_list[(int)sfx];
        audioSources[cursor].Play();

        cursor++;
        if (cursor > 19)
        {
            cursor = 0;
        }
    }

    [SerializeField]
    private List<AudioClip> bgmList;

    public void ChangeBGM(BGM_Type newBGM)
    {
        StartCoroutine(ChangeBGMClip(bgmList[(int)newBGM]));
    }

    float current;
    float percent;

    // ���� BGM�� �Ҹ��� �ٿ����鼭 �ű� BGM�� �Ҹ��� Ű�� ��ü�ϴ� �ڷ�ƾ
    IEnumerator ChangeBGMClip(AudioClip newClip)
    {
        current = 0f;
        percent = 0f;
        while(percent < 1f) // ���� BGM�� �Ҹ��� �ٿ�����
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmSource.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        bgmSource.clip = newClip;
        bgmSource.Play();
        current = 0f;
        percent = 0f;

        while(percent < 1f) // �ű� BGM�� �Ҹ��� Ű������
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmSource.volume = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }
    }


}
