using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI skillName;
    [SerializeField]
    private TextMeshProUGUI balanceText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private TextMeshProUGUI energyText;


    [SerializeField]
    private GameObject upgradeBtnObj;

    private SkillButton skillButton;
    private int uPrice;
    private int balance;

    public void InitToolTip(SkillButton skill, int price)
    {
        skillButton = skill;
        skillName.text = skillButton.TYPE.ToString();

        priceText.text = "/ " + price.ToString();
        uPrice = price;
        balance = PlayerPrefs.GetInt(Save_Type.st_Gold.ToString());
        balanceText.text = balance.ToString();

        if(balance >= uPrice)
        {
            balanceText.color = Color.white;
        }
        else
        {
            balanceText.color = Color.red;
        }


        if(skillButton.STATE == EnchantState.ES_Enable)
        {
            upgradeBtnObj.SetActive(true);
        }
        else
        {
            upgradeBtnObj.SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.SetActive(false);
        }
    }

    public void UpgradeBtn()
    {
        if(balance >= uPrice && skillButton)
        {
            balance -= uPrice;
            PlayerPrefs.SetInt(Save_Type.st_Gold.ToString(), balance); // 세이브 파일(골드) 갱신

            skillButton.UpgradeSkill();
        }

    }

}
