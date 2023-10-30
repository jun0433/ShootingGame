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
        loadingBar.fillAmount = 0f;
        StartCoroutine("LoadAsyncScene");
    }


    // CPU의 일부만 사용해서 다음씬을 로딩
    // 렉걸린거처럼 화면이 멈춰져 있으면 안됨
    // 비동기로딩 (백그라운드로딩)
    IEnumerator LoadAsyncScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(Save_Type.st_SceneName.ToString()));

        asyncOperation.allowSceneActivation = false; // 로딩이 완료되면 바로 다음씬 활성화. 

        float timeC = 0f;

        while (!asyncOperation.isDone) // isDone = 로딩의 완료 여부. 
        { // 로딩이 완료되지 않았을때 반복. 
            yield return null;
            timeC += Time.deltaTime;

            if (asyncOperation.progress >= 0.9f) // 90%이상 로딩 완료. 
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount >= 0.999f) // 바가 가득 채워졌을떄,
                    asyncOperation.allowSceneActivation = true; // 다음씬으로 넘어간다. 
            }
            else // progress가 0.9작은경우,  열씸히 씬을 조각조각 로딩을 하고 있는 과정. 
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncOperation.progress,
                                        timeC);
                if (loadingBar.fillAmount >= asyncOperation.progress)
                    timeC = 0f;
            }
        }
    }
}
