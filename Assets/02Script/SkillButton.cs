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

// ��ų ����
public enum EnchantState
{
    ES_Learn, // ��� ��
    ES_Enable, // ��� �� �ִ� ��
    ES_Disable, // ������ ��
}

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private GameObject block_1;
    [SerializeField]
    private GameObject block_2;

    [SerializeField]
    private SkillButton prevSkill; // ���� ��ų.
    [SerializeField]
    private SkillButton nextSkill; // ���� ��ų

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
    private int gradeLevel; // ��� �� �ִ� ���� 



    public void InitSkillButton(bool isLearn, int playerlevel)
    {

        // �⺻���� ��� ����� �ʰ� ��� �� ���� ����
        block_1.SetActive(true);
        block_2.SetActive(true);
        STATE = EnchantState.ES_Disable;


        // ��� ����
        if (isLearn)
        {
            block_1.SetActive(false);
            block_2.SetActive(false);
            STATE = EnchantState.ES_Learn;
        }
        else // ����� ���� ����
        {
            // ���� ��ų�� �ִ��� Ȯ��
            if(prevSkill != null)
            {
                // ���� ��ų�� ����� ��� �� �ִ� �������� Ȯ��.
                if(prevSkill.STATE == EnchantState.ES_Learn && playerlevel >= gradeLevel)
                {
                    block_2.SetActive(false);
                    STATE = EnchantState.ES_Enable;
                }
            }
            else if(playerlevel >= gradeLevel) // ������ �Ǵµ� ���ེų�� ����
            {
                block_2.SetActive(false);
                STATE = EnchantState.ES_Enable;
            }
        }

    }

    [SerializeField]
    private GameObject toolTipObj;

    // �ش� ��ų ��ư�� Ŭ���� ���� ��
    public void OnClickBtn()
    {
        toolTipObj.SetActive(true);
        toolTipObj.transform.parent = transform;
        toolTipObj.transform.localPosition = new Vector3(270f, 0f, 0f); // ��� ��ǥ��(�θ�κ���)
        toolTipObj.GetComponent<ToolTip>().InitToolTip(this, 500);
    }


    // ��ų ����� ó��
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
