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

    // Scene 로딩이 완료가 될 때 호출되는 이벤트. 파라메터로 들어오는 level은 projectSetting 확인
    /*private void OnLevelWasLoaded(int level)
    {
        
    }*/

    private void InitLobbyScene()
    {//UI의 갱신은 최소한의 이벤트로만 처리
        levelText.text = PlayerPrefs.GetInt(Save_Type.st_Level.ToString()).ToString();
        goldText.text = PlayerPrefs.GetInt(Save_Type.st_Gold.ToString()).ToString();
        expBar.fillAmount = PlayerPrefs.GetInt(Save_Type.st_Exp.ToString()) / 300f;

        // 재접속을 했을 때 저장된 옵션 값을 참조해서 초기 세팅
        SFX_ValueChange(PlayerPrefs.GetFloat(Save_Type.SFX_Param.ToString()));
        BGM_ValueChange(PlayerPrefs.GetFloat(Save_Type.BGM_Param.ToString()));


        Skill_Type skill_Index;

        for(int i = 0; i < skillBtnList.Count; i++)
        {
            skill_Index = (Skill_Type)i;
            if(0 < PlayerPrefs.GetInt(skill_Index.ToString())) // 0번 배우지 않은 상태
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
                Debug.Log("LobbyManager.cs - ActiveMenu 값 변경 오류");
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
        if(activeMenu == i) // 열린 팝업의 버튼이 눌린 경우
        {
            LeanTween.moveLocalY(popupObjList[activeMenu], popupOriPos.y, 0.5f).setEaseOutCirc(); // 0.5초 동안 빠르게 org 포지션으로 이동
            ActiveMenu = 0;
        }
        else // 열린 팝업과 눌린 버튼이 다른 경우
        {
            if(activeMenu != 0) // 열린 팝업이 있는 경우
            {
                LeanTween.moveLocalY(popupObjList[activeMenu], popupOriPos.y, 0.5f).setEaseOutCirc();
            }

            ActiveMenu = i;
            LeanTween.moveLocalY(popupObjList[activeMenu], 0f, 0.5f).setEaseOutCirc();
        }
    }

    public void OnClick_GameStart()
    {
        PlayerPrefs.SetString(Save_Type.st_SceneName.ToString(), Scene.Battle.ToString()); // 다음씬 이름 저장
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

    // dB를 저장할 변수
    private float valueF;

    public void SFX_ValueChange(float value)
    {
        Debug.Log("효과음 옵션 슬라이드 변화: " + value);
        PlayerPrefs.SetFloat(Save_Type.SFX_Param.ToString(), value); // 세이브 파일 저장.
        ChangeVolume(sfx_Text, sfx_Slider, Save_Type.SFX_Param, value);
    }

    public void BGM_ValueChange(float value)
    {
        Debug.Log("배경음 옵션 슬라이드 변화: " + value);
        PlayerPrefs.SetFloat(Save_Type.BGM_Param.ToString(), value); // 세이브 파일 저장.
        ChangeVolume(bgm_Text, bgm_Slider, Save_Type.BGM_Param, value);
    }

    // 볼륨 변화 함수(볼륨 text, slider, 저장 타입(볼륨 유지를 위해), 볼륨값)
    private void ChangeVolume(TextMeshProUGUI text, Slider slider, Save_Type save_Type, float newVolume)
    {
        text.text = newVolume.ToString("N2");
        slider.value = newVolume;
        valueF = newVolume * 30f - 30f; // dB는 0이여도 정상적으로 들림
        // BGM, SFX_Param
        audio_Master.SetFloat(save_Type.ToString(), valueF);

    }


    [SerializeField]
    private List<SkillButton> skillBtnList; // 스킬 버튼을 관리하는 리스트


}
