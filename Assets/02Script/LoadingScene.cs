using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;

    private void Awake()
    {
        loadingBar.fillAmount = 0;
        StartCoroutine("LoadAsyncScene");
    }


    // CPU�� �Ϻθ� ����ؼ� �������� �ε�
    // ���ɸ���ó�� ȭ���� ������ ������ �ȵ�
    // �񵿱�ε� (��׶���ε�)
    IEnumerator LoadAsyncScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(Save_Type.st_SceneName.ToString()));
    }
}
