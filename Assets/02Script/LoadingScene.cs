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


    // CPU�� �Ϻθ� ����ؼ� �������� �ε�
    // ���ɸ���ó�� ȭ���� ������ ������ �ȵ�
    // �񵿱�ε� (��׶���ε�)
    IEnumerator LoadAsyncScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(Save_Type.st_SceneName.ToString()));

        asyncOperation.allowSceneActivation = false; // �ε��� �Ϸ�Ǹ� �ٷ� ������ Ȱ��ȭ. 

        float timeC = 0f;

        while (!asyncOperation.isDone) // isDone = �ε��� �Ϸ� ����. 
        { // �ε��� �Ϸ���� �ʾ����� �ݺ�. 
            yield return null;
            timeC += Time.deltaTime;

            if (asyncOperation.progress >= 0.9f) // 90%�̻� �ε� �Ϸ�. 
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount >= 0.999f) // �ٰ� ���� ä��������,
                    asyncOperation.allowSceneActivation = true; // ���������� �Ѿ��. 
            }
            else // progress�� 0.9�������,  ������ ���� �������� �ε��� �ϰ� �ִ� ����. 
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncOperation.progress,
                                        timeC);
                if (loadingBar.fillAmount >= asyncOperation.progress)
                    timeC = 0f;
            }
        }
    }
}
