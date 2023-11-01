using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Skill_Type
{
    ST_Skill_01,
    ST_Skill_02,
    ST_Skill_03,
    ST_Skill_04,
    ST_Skill_05,
    ST_Skill_06,


}

// 스킬 상태
public enum EnchantState
{
    ES_Learn, // 배운 것
    ES_Enable, // 배울 수 있는 것
    ES_Disable, // 못배우는 것
}

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private GameObject block_1;
    [SerializeField]
    private GameObject block_2;

    [SerializeField]
    private SkillButton prevSkill; // 이전 스킬.
    [SerializeField]
    private SkillButton nextSkill; // 다음 스킬

    private EnchantState curState;

    public EnchantState STATE
    {
        get => curState;
        set => curState = value;
    }

    [SerializeField]
    private Skill_Type curType;
    public Skill_Type TYPE
    {
        get => curType;
    }

    [SerializeField]
    private int gradeLevel; // 배울 수 있는 레벨 



    public void InitSkillButton(bool isLearn, int playerlevel)
    {

        // 기본값은 모두 배우지 않고 배울 수 없는 상태
        block_1.SetActive(true);
        block_2.SetActive(true);
        STATE = EnchantState.ES_Disable;


        // 배운 상태
        if (isLearn)
        {
            block_1.SetActive(false);
            block_2.SetActive(false);
            STATE = EnchantState.ES_Learn;
        }
        else // 배우지 못한 상태
        {
            // 선행 스킬이 있는지 확인
            if(prevSkill != null)
            {
                // 선행 스킬을 배웠고 배울 수 있는 레벨인지 확인.
                if(prevSkill.STATE == EnchantState.ES_Learn && playerlevel >= gradeLevel)
                {
                    block_2.SetActive(false);
                    STATE = EnchantState.ES_Enable;
                }
            }
            else if(playerlevel >= gradeLevel) // 레벨은 되는데 선행스킬이 없음
            {
                block_2.SetActive(false);
                STATE = EnchantState.ES_Enable;
            }
        }

    }

    [SerializeField]
    private GameObject toolTipObj;

    // 해당 스킬 버튼이 클릭이 됐을 때
    public void OnClickBtn()
    {
        toolTipObj.SetActive(true);
        toolTipObj.transform.parent = transform;
        toolTipObj.transform.localPosition = new Vector3(270f, 0f, 0f); // 상대 좌표계(부모로부터)
        toolTipObj.GetComponent<ToolTip>().InitToolTip(this, 500);
    }


    // 스킬 배움을 처리
    public void UpgradeSkill()
    {
        PlayerPrefs.SetInt(TYPE.ToString(), 1);

        InitSkillButton(true, 7);
        if(nextSkill != null)
        {
            nextSkill.InitSkillButton(false, 7);
        }
    }

}
