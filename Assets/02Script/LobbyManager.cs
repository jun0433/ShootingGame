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
}
