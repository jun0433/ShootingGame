using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager inst;

    public static GameManager Inst
    {
        get => inst;
    }

    private void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            inst = this;
        }

        if(obj = GameObject.Find("Player"))
        {
            if(!obj.TryGetComponent<PlayerController>(out playerC))
            {
                Debug.Log("GameManager.cs - Awake() - playerC 참조 실패");
            }
        }

        if (obj = GameObject.Find("EnemySpawnManager"))
        {
            if (!obj.TryGetComponent<EnemySpawner>(out enemyS))
            {
                Debug.Log("GameManager.cs - Awake() - enemyS 참조 실패");
            }
        }

        if (obj = GameObject.Find("MeteoriteManager"))
        {
            if (!obj.TryGetComponent<MeteoriteManager>(out meteoriteM))
            {
                Debug.Log("GameManager.cs - Awake() - meteoriteM 참조 실패");
            }
        }

        Invoke("InitGame", 0.1f); // 스테이지 로직의 시작점

    }

    private GameObject obj;
    private PlayerController playerC;
    private EnemySpawner enemyS;
    private MeteoriteManager meteoriteM;


    private void InitGame()
    {
        scoreText.text = "0";
        boomText.text = "x5";

        playerC.InitController();
        enemyS.InitSpawner();
        meteoriteM.InitMeteorite();
    }



    [SerializeField]
    private List<GameObject> heartImage;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI boomText;
    [SerializeField]
    private GameObject stageClearPopup;


    public void changeBoomText(int count)
    {
        boomText.text = "X" + count.ToString();
    }

    private int gameScore = 0;

    public void AddScore(int point)
    {
        gameScore += point;
        scoreText.text = gameScore.ToString();
    }

    public void ChangeHeart(bool isHealing, int HeartPoint)
    {
        if (isHealing)
        {
            heartImage[HeartPoint - 1].SetActive(true);
        }
        else
        {
            heartImage[HeartPoint].SetActive(false);
        }
    }

    public void StageClear()
    {
        stageClearPopup.transform.localPosition = Vector3.zero;
    }
}
