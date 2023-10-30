using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum Save_Type
{
    st_NickName,
    st_SceneName,
    st_SFX,
    st_BGM,
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
        {// 세이브 데이터가 없음
            welcomeText.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else
        {// 세이브 데이터가 있음
            welcomeText.text = PlayerPrefs.GetString(Save_Type.st_NickName.ToString()) + "님 환영합니다.";

            welcomeText.gameObject.SetActive(true);
            haveUserInfo = true;
        }
    }

    public void LoginBtn()
    {
        if(!haveUserInfo) // 계정이 없을 경우
        {
            if(inputField.text.Length > 2)
            {
                // 계정 생성
                CreateUserData(inputField.text);
            }
            haveUserInfo = true;
        }

        if(haveUserInfo)
        {
            PlayerPrefs.SetString(Save_Type.st_SceneName.ToString(), Scene.Lobby.ToString()); // 파일에 로비씬 이름 저장

            // 씬 로딩. 다음씬 넘어가기.
            SceneManager.LoadScene(Scene.Loading.ToString());  // 실제로 로딩씬 이동
        }
    }

    private void CreateUserData(string userName)
    {
        PlayerPrefs.SetString(Save_Type.st_NickName.ToString(), userName);
        PlayerPrefs.SetInt(Save_Type.st_Level.ToString(), 1);
        PlayerPrefs.SetInt(Save_Type.st_Exp.ToString(), 0);
        PlayerPrefs.SetInt(Save_Type.st_Gold.ToString(), 5000);

        PlayerPrefs.SetFloat(Save_Type.st_SFX.ToString(), 1.0f);
        PlayerPrefs.SetFloat(Save_Type.st_BGM.ToString(), 1.0f);
    }

    public void InitUserData()
    {
        PlayerPrefs.DeleteAll();
        InitLoginScene();
    }

}
