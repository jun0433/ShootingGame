using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossChar : MonoBehaviour, IDamage
{
    [SerializeField]
    private int maxHP;
    public int MAXHP
    {
        get => maxHP;
    }
    [SerializeField]
    private int curHP;
    public int CURHP
    {
        get => curHP;
    }

    [SerializeField]
    private Image imgFill;

    [SerializeField]
    private TextMeshProUGUI textName;

    [SerializeField]
    private EnemySpawner spawnManager;

    private BossAI bossAI;

    private bool isAlive;
    private SpriteRenderer sr;

    private void Awake()
    {
        if(!TryGetComponent<SpriteRenderer>(out sr)){
            Debug.Log("BossChar.cs - Awake() - sr 참조 실패");
        }
        GameObject obj = GameObject.Find("EnemySpawnManager");
        if(obj != null)
        {
            if(!obj.TryGetComponent<EnemySpawner>(out spawnManager))
            {
                Debug.Log("BossChar.cs - Awake() - spawnManager 참조 실패");
            }
        }

        if (!TryGetComponent<BossAI>(out bossAI))
        {
            Debug.Log("BossChar.cs - Awake() - bossAI 참조실패");
        }

    }

    public void InitBoss(string name, int newHP)
    {
        curHP = maxHP = newHP;
        sr.color = Color.white;
        isAlive = true;
        textName.text = name;
    }

    // Boss가 데미지를 입는 함수
    public void TakeDamge(int damage)
    {
        if (isAlive)
        {
            curHP = curHP - Weapon.Inst.PLAYERDAMAGE;
            StopCoroutine("HitColor");
            StartCoroutine("HitColor");

            imgFill.fillAmount = Mathf.Clamp((float)curHP / maxHP, 0f, 1f);

            if(curHP <= 0)
            {
                OnDie();
            }
        }

    }


    // 보스가 죽었을 때 작동하는 함수
    private void OnDie()
    {
        isAlive = false;
        //gameObject.SetActive(false);
        transform.position = new Vector3(-8f, -2f, 0f);
        GameManager.Inst.AddKillCount(true);
        GameManager.Inst.AddScore(50);
        EnemySpawner.Inst.bossHPUI.SetActive(false);
        bossAI.ChangeState(BossState.BS_Stop);

        // 다음 웨이브를 시작
        spawnManager.StartWave();
    }

    private IEnumerator HitColor()
    {
        sr.color = Color.red;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        if (gameObject.activeSelf) // 활성화 되어 있다면(죽지 않음)
        {
            sr.color = Color.white;
        }
    }
}
