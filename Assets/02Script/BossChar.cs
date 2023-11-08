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
    }

    public void InitBoss(string name, int newHP)
    {
        curHP = maxHP = newHP;
        sr.color = Color.white;
        isAlive = true;
        textName.text = name;
    }

    public void TakeDamge(int damage)
    {
        if (isAlive)
        {
            curHP -= 1;
            StopCoroutine("HitColor");
            StartCoroutine("HitColor");

            imgFill.fillAmount = Mathf.Clamp((float)curHP / maxHP, 0f, 1f);

            if(curHP <= 0)
            {
                OnDie();
            }
        }

    }

    private void OnDie()
    {
        isAlive = false;
        gameObject.SetActive(false);
        transform.position = new Vector3(0f, 7f, 0f);
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
