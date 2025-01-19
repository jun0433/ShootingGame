using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : ObjectPool_Label
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float boomDelay = 1.5f; // 플레이어가 발사하면 중앙까지 날아기는 시간.

    private Animator anim;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("PlayerBoom.cs - Awake() - anim 참조 실패");
        }

    }

    public override void InitInfo()
    {
        base.InitInfo();
        StartCoroutine("MoveToCenter");
    }

    private Vector3 startPos;
    private Vector3 endPos = Vector3.zero;
    private float current;
    private float percent;

    private IEnumerator MoveToCenter()
    {

        startPos = GameObject.Find("Player").transform.position;

        current = 0f;
        percent = 0f;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));
            yield return null;
        }
        // 이동이 완료된 상황
        anim.SetTrigger("doFire");


    }

    /// <summary>
    /// 소환된 Enemy를 제거할 수 있는 폭탄을 사용하는 함수
    /// </summary>
    public void Explosion()
    {
        Debug.Log("주변 몬스터 처치");

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemys.Length; i++)
        {
            if(enemys[i].TryGetComponent<EnemyChar>(out EnemyChar enemyChar))
            {
                enemyChar.OnDie();
            }
            else if(enemys[i].TryGetComponent<ObjectPool_Label>(out ObjectPool_Label label))
            {
                label.Push();
            }
        }
        Push(); // 폭탄 반환
    }

}
