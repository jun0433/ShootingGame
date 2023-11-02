using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
    GS_Init, // ���� ���� ��
    GS_Play, // ���� ������
    GS_PlayerDie, // �÷��̾� ���
    
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
                Debug.Log("GameManager.cs - Awake() - playerC ���� ����");
            }
        }

        if (obj = GameObject.Find("EnemySpawnManager"))
        {
            if (!obj.TryGetComponent<EnemySpawner>(out enemyS))
            {
                Debug.Log("GameManager.cs - Awake() - enemyS ���� ����");
            }
        }

        if (obj = GameObject.Find("MeteoriteManager"))
        {
            if (!obj.TryGetComponent<MeteoriteManager>(out meteoriteM))
            {
                Debug.Log("GameManager.cs - Awake() - meteoriteM ���� ����");
            }
        }
        if(obj = GameObject.Find("FadeImg"))
        {
            if (!obj.TryGetComponent<FadeInOut>(out fade))
            {
                Debug.Log("GameManager.cs - Awake() - fade ���� ����");
            }
        }

        curState = GameState.GS_Init;
        // Invoke("InitGame", 0.1f); // �������� ������ ������

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
        // �� �ε��� �Ϸᰡ �Ǿ��� ��
        scoreText.text = "0";
        boomText.text = "x5";
        // ���̵� ó��.
        fade.Fade_InOut(true, 1.5f);
        // ���� ����
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


    private int countNormal; // �Ϲ� ���� óġ ī��Ʈ
    public int CountN
    {
        get => countNormal;
    }
    private int countBoss; // ���� ���� óġ ī��Ʈ
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
        Debug.Log("���� óġ: " + countNormal * 3);
        totalGold += countBoss * 100;
        Debug.Log("���� óġ: " + countBoss * 100);
        totalGold += gameScore / 10;
        Debug.Log("���ھ� óġ: " + gameScore / 10);

        totalGold += PlayerPrefs.GetInt(Save_Type.st_Gold.ToString());

        PlayerPrefs.SetInt(Save_Type.st_Gold.ToString(), totalGold);

        int totalEXP = 0;
        totalEXP += countNormal * 3;
        Debug.Log("���� óġ: " + countNormal * 3);
        totalEXP += countBoss * 100;
        Debug.Log("���� óġ: " + countBoss * 100);


        totalEXP += PlayerPrefs.GetInt(Save_Type.st_Exp.ToString());

        int curLevel = PlayerPrefs.GetInt(Save_Type.st_Level.ToString());

        curLevel += (totalEXP / 300);
        PlayerPrefs.SetInt(Save_Type.st_Level.ToString(), curLevel);

        totalEXP = totalEXP % 300; // �������ϰ� ���� ����ġ
        PlayerPrefs.SetInt(Save_Type.st_Exp.ToString(), totalEXP);
    }
}
