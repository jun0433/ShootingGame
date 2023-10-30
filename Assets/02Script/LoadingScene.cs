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


    // CPU의 일부만 사용해서 다음씬을 로딩
    // 렉걸린거처럼 화면이 멈춰져 있으면 안됨
    // 비동기로딩 (백그라운드로딩)
    IEnumerator LoadAsyncScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(Save_Type.st_SceneName.ToString()));
    }
}
