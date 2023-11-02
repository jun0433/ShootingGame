using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
    GS_Init, // 게임 시작 전
    GS_Play, // 게임 진행중
    GS_PlayerDie, // 플레이어 사망
    
}

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

        if (obj = GameObject.Find("Player"))
        {
            if (!obj.TryGetComponent<PlayerController>(out playerC))
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
        if(obj = GameObject.Find("FadeImg"))
        {
            if (!obj.TryGetComponent<FadeInOut>(out fade))
            {
                Debug.Log("GameManager.cs - Awake() - fade 참조 실패");
            }
        }

        curState = GameState.GS_Init;
        // Invoke("InitGame", 0.1f); // 스테이지 로직의 시작점

    }

    private GameObject obj;
    private PlayerController playerC;
    private EnemySpawner enemyS;
    private MeteoriteManager meteoriteM;

    private GameState curState;

    public GameState CurState
    {
        get => curState;
    }

    private FadeInOut fade;

    private void OnLevelWasLoaded(int level)
    {
        // 씬 로딩이 완료가 되었을 때
        scoreText.text = "0";
        boomText.text = "x5";
        // 페이드 처리.
        fade.Fade_InOut(true, 1.5f);
        // 게임 시작
        Invoke("InitGame", 2f);
    }

    private void InitGame()
    {

        playerC.InitController();
        enemyS.InitSpawner();
        meteoriteM.InitMeteorite();
        gameScore = 0;
        countNormal = 0;
        countBoss = 0;


        curState = GameState.GS_Play;
    }



    [SerializeField]
    private List<GameObject> heartImage;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI boomText;
    [SerializeField]
    private GameObject stageClearPopup;


    private int countNormal; // 일반 몬스터 처치 카운트
    public int CountN
    {
        get => countNormal;
    }
    private int countBoss; // 보스 몬스터 처치 카운트
    public int CountB
    {
        get => countBoss;
    }


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

    public void AddKillCount(bool isBoss)
    {
        if (isBoss)
        {
            countBoss++;
        }
        else
        {
            countNormal++;
        }
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
        curState = GameState.GS_PlayerDie;
        enemyS.StopWave();
        meteoriteM.StopMeteorite();
        stageClearPopup.GetComponent<StageClearManager>().SetStageClearPopup();
    }

    public void StageResult()
    {
        int totalGold = 0;
        totalGold += countNormal * 3;
        Debug.Log("몬스터 처치: " + countNormal * 3);
        totalGold += countBoss * 100;
        Debug.Log("보스 처치: " + countBoss * 100);
        totalGold += gameScore / 10;
        Debug.Log("스코어 처치: " + gameScore / 10);

        totalGold += PlayerPrefs.GetInt(Save_Type.st_Gold.ToString());

        PlayerPrefs.SetInt(Save_Type.st_Gold.ToString(), totalGold);

        int totalEXP = 0;
        totalEXP += countNormal * 3;
        Debug.Log("몬스터 처치: " + countNormal * 3);
        totalEXP += countBoss * 100;
        Debug.Log("보스 처치: " + countBoss * 100);


        totalEXP += PlayerPrefs.GetInt(Save_Type.st_Exp.ToString());

        int curLevel = PlayerPrefs.GetInt(Save_Type.st_Level.ToString());

        curLevel += (totalEXP / 300);
        PlayerPrefs.SetInt(Save_Type.st_Level.ToString(), curLevel);

        totalEXP = totalEXP % 300; // 레벨업하고 남은 경험치
        PlayerPrefs.SetInt(Save_Type.st_Exp.ToString(), totalEXP);
    }
}
