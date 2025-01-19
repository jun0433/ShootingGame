using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : ObjectPool_Label
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float boomDelay = 1.5f; // �÷��̾ �߻��ϸ� �߾ӱ��� ���Ʊ�� �ð�.

    private Animator anim;

    private void Awake()
    {
        if(!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("PlayerBoom.cs - Awake() - anim ���� ����");
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
        // �̵��� �Ϸ�� ��Ȳ
        anim.SetTrigger("doFire");


    }

    /// <summary>
    /// ��ȯ�� Enemy�� ������ �� �ִ� ��ź�� ����ϴ� �Լ�
    /// </summary>
    public void Explosion()
    {
        Debug.Log("�ֺ� ���� óġ");

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
        Push(); // ��ź ��ȯ
    }

}
