using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public enum MenuType
{
    MenuType_Enchant = 1,
    MenuType_Option = 5,
}

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI EnergyText;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private Image expBar;


    private void Awake()
    {
        InitLobbyScene();
    }

    // Scene �ε��� �Ϸᰡ �� �� ȣ��Ǵ� �̺�Ʈ. �Ķ���ͷ� ������ level�� projectSetting Ȯ��
    /*private void OnLevelWasLoaded(int level)
    {
        
    }*/

    private void InitLobbyScene()
    {//UI�� ������ �ּ����� �̺�Ʈ�θ� ó��
        levelText.text = PlayerPrefs.GetInt(Save_Type.st_Level.ToString()).ToString();
        goldText.text = PlayerPrefs.GetInt(Save_Type.st_Gold.ToString()).ToString();
        expBar.fillAmount = PlayerPrefs.GetInt(Save_Type.st_Exp.ToString()) / 300f;

        // �������� ���� �� ����� �ɼ� ���� �����ؼ� �ʱ� ����
        SFX_ValueChange(PlayerPrefs.GetFloat(Save_Type.SFX_Param.ToString()));
        BGM_ValueChange(PlayerPrefs.GetFloat(Save_Type.BGM_Param.ToString()));


        Skill_Type skill_Index;

        for(int i = 0; i < skillBtnList.Count; i++)
        {
            skill_Index = (Skill_Type)i;
            if(0 < PlayerPrefs.GetInt(skill_Index.ToString())) // 0�� ����� ���� ����
            {
                skillBtnList[i].InitSkillButton(true, 7);
            }
            else
            {
                skillBtnList[i].InitSkillButton(false, 7);
            }
        }
    }

    private int activeMenu = 0;

    public int ActiveMenu
    {
        set
        {
            if(value < 0 || value > 5)
            {
                Debug.Log("LobbyManager.cs - ActiveMenu �� ���� ����");
                activeMenu = 0;
            }
            else
            {
                activeMenu = value;
            }
        }
    }

    [SerializeField]
    private List<GameObject> popupObjList;
    private Vector3 popupOriPos = new Vector3(0f, -1800f, 0f);

    public void OnClickEvent(int i)
    {
        if(activeMenu == i) // ���� �˾��� ��ư�� ���� ���
        {
            LeanTween.moveLocalY(popupObjList[activeMenu], popupOriPos.y, 0.5f).setEaseOutCirc(); // 0.5�� ���� ������ org ���������� �̵�
            ActiveMenu = 0;
        }
        else // ���� �˾��� ���� ��ư�� �ٸ� ���
        {
            if(activeMenu != 0) // ���� �˾��� �ִ� ���
            {
                LeanTween.moveLocalY(popupObjList[activeMenu], popupOriPos.y, 0.5f).setEaseOutCirc();
            }

            ActiveMenu = i;
            LeanTween.moveLocalY(popupObjList[activeMenu], 0f, 0.5f).setEaseOutCirc();
        }
    }

    public void OnClick_GameStart()
    {
        PlayerPrefs.SetString(Save_Type.st_SceneName.ToString(), Scene.Battle.ToString()); // ������ �̸� ����
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    [SerializeField]
    private TextMeshProUGUI sfx_Text;
    [SerializeField]
    private Slider sfx_Slider;

    [SerializeField]
    private TextMeshProUGUI bgm_Text;
    [SerializeField]
    private Slider bgm_Slider;

    [SerializeField]
    private AudioMixer audio_Master;

    // dB�� ������ ����
    private float valueF;

    public void SFX_ValueChange(float value)
    {
        Debug.Log("ȿ���� �ɼ� �����̵� ��ȭ: " + value);
        PlayerPrefs.SetFloat(Save_Type.SFX_Param.ToString(), value); // ���̺� ���� ����.
        ChangeVolume(sfx_Text, sfx_Slider, Save_Type.SFX_Param, value);
    }

    public void BGM_ValueChange(float value)
    {
        Debug.Log("����� �ɼ� �����̵� ��ȭ: " + value);
        PlayerPrefs.SetFloat(Save_Type.BGM_Param.ToString(), value); // ���̺� ���� ����.
        ChangeVolume(bgm_Text, bgm_Slider, Save_Type.BGM_Param, value);
    }

    // ���� ��ȭ �Լ�(���� text, slider, ���� Ÿ��(���� ������ ����), ������)
    private void ChangeVolume(TextMeshProUGUI text, Slider slider, Save_Type save_Type, float newVolume)
    {
        text.text = newVolume.ToString("N2");
        slider.value = newVolume;
        valueF = newVolume * 30f - 30f; // dB�� 0�̿��� ���������� �鸲
        // BGM, SFX_Param
        audio_Master.SetFloat(save_Type.ToString(), valueF);

    }


    [SerializeField]
    private List<SkillButton> skillBtnList; // ��ų ��ư�� �����ϴ� ����Ʈ


}
