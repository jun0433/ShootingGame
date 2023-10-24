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


        scoreText.text = "0";
        boomText.text = "x5";
    }

    [SerializeField]
    private List<GameObject> heartImage;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI boomText;


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
}
