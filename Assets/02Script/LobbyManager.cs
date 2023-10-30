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
}
