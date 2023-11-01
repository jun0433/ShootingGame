using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum Save_Type
{
    st_NickName,
    st_SceneName,
    SFX_Param,
    BGM_Param,
    st_Level,
    st_Exp,
    st_Gold,

}

public enum Scene
{
    Title,
    Lobby,
    Loading,
    Battle
}

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    private bool haveUserInfo;

    [SerializeField]
    private TextMeshProUGUI welcomeText;

    private void  Awake()
    {
        InitLoginScene();
    }


    private void InitLoginScene()
    {
        if(PlayerPrefs.GetString(Save_Type.st_NickName.ToString()).Length < 2)
        {// ���̺� �����Ͱ� ����
            welcomeText.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else
        {// ���̺� �����Ͱ� ����
            welcomeText.text = PlayerPrefs.GetString(Save_Type.st_NickName.ToString()) + "�� ȯ���մϴ�.";

            welcomeText.gameObject.SetActive(true);
            haveUserInfo = true;
        }
    }

    public void LoginBtn()
    {
        if(!haveUserInfo) // ������ ���� ���
        {
            if(inputField.text.Length > 2)
            {
                // ���� ����
                CreateUserData(inputField.text);
            }
            haveUserInfo = true;
        }

        if(haveUserInfo)
        {
            PlayerPrefs.SetString(Save_Type.st_SceneName.ToString(), Scene.Lobby.ToString()); // ���Ͽ� �κ�� �̸� ����

            // �� �ε�. ������ �Ѿ��.
            SceneManager.LoadScene(Scene.Loading.ToString());  // ������ �ε��� �̵�
        }
    }

    private void CreateUserData(string userName)
    {
        PlayerPrefs.SetString(Save_Type.st_NickName.ToString(), userName);
        PlayerPrefs.SetInt(Save_Type.st_Level.ToString(), 1);
        PlayerPrefs.SetInt(Save_Type.st_Exp.ToString(), 0);
        PlayerPrefs.SetInt(Save_Type.st_Gold.ToString(), 5000);

        PlayerPrefs.SetFloat(Save_Type.SFX_Param.ToString(), 1.0f);
        PlayerPrefs.SetFloat(Save_Type.BGM_Param.ToString(), 1.0f);

        PlayerPrefs.SetInt(Skill_Type.ST_Skill_01.ToString(), 0);
        PlayerPrefs.SetInt(Skill_Type.ST_Skill_02.ToString(), 0);
        PlayerPrefs.SetInt(Skill_Type.ST_Skill_03.ToString(), 0);
        PlayerPrefs.SetInt(Skill_Type.ST_Skill_04.ToString(), 0);
        PlayerPrefs.SetInt(Skill_Type.ST_Skill_05.ToString(), 0);
        PlayerPrefs.SetInt(Skill_Type.ST_Skill_06.ToString(), 0);
    }

    public void InitUserData()
    {
        PlayerPrefs.DeleteAll();
        InitLoginScene();
    }

}
