using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageClearManager : MonoBehaviour
{

    public void ReturnLobby()
    {
        // ��� ����

        // ���̵� ȿ�� ����

        // �� ����
        Invoke("LoadScene", 3f);
    }

    private void LoadScene()
    {
        GameManager.Inst.StageResult(); // ��带 ������Ű�� �Լ�
        SceneManager.LoadScene("Lobby"); // "Lobby"�� �ε�
    }

    [SerializeField]
    private GameObject title;

    [SerializeField]
    private GameObject popupBack;
    [SerializeField]
    private GameObject homeBtn;
    [SerializeField]
    private GameObject killCount;
    [SerializeField]
    private GameObject bossCount;
    [SerializeField]
    private TextMeshProUGUI killText;
    [SerializeField]
    private TextMeshProUGUI bossText;

    [SerializeField]
    private GameObject star01;
    [SerializeField]
    private GameObject star02;
    [SerializeField]
    private GameObject star03;


    private void Awake()
    {
        //SetStageClearPopup();
    }

    private void SetData()
    {
        killText.GetComponent<TextMeshProUGUI>().text = GameManager.Inst.CountN.ToString();
        bossText.GetComponent<TextMeshProUGUI>().text = GameManager.Inst.CountB.ToString();
    }

    public void SetStageClearPopup()
    {
        SetData();
        // LeanTween�� �̿��ؼ� scale�� ����(��� ������Ʈ, ũ��, �ð�).setEase(LanTween Ÿ�� ����).setDelay(������ �ð� ����).setOnComplete(������ �θ� �Լ�)
        LeanTween.scale(title, new Vector3(1.5f, 1.5f, 1.5f), 2f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f).setOnComplete(LevelComplete);

        LeanTween.moveLocalY(title, 450f, 0.5f).setEase(LeanTweenType.easeInOutCubic).setDelay(2f);
    }

    private void LevelComplete()

    {
        LeanTween.moveLocal(popupBack, new Vector3(0f, 0f, 0f), 0.7f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(StarOn);

        LeanTween.scale(homeBtn, new Vector3(1f, 1f, 1f), 2f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(killCount.GetComponent<RectTransform>(), new Vector3(1f, 1f, 1f), 2f).setDelay(1.2f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(bossCount.GetComponent<RectTransform>(), new Vector3(1f, 1f, 1f), 2f).setDelay(1.4f).setEase(LeanTweenType.easeOutElastic);

    }

    private void StarOn()

    {
        LeanTween.scale(star02, new Vector3(1f, 1f, 1f), 2f).setDelay(0.1f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(star03, new Vector3(1f, 1f, 1f), 2f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(star01, new Vector3(1.5f, 1.5f, 1.5f), 2f).setDelay(0.3f).setEase(LeanTweenType.easeOutElastic);

    }

}
