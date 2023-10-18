using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isInit = false;        // ���� ����ϱ� ���� ������ ��������. 
    private GameObject projectilePrefab;  // ������Ÿ���� � �������� �̿��Ͽ� ��������.
    private float attackRate;   // ���ݰ� ���� ���̿� �ð�. 

    public void InitWeapon(GameObject projectile, float rate)
    {
        isInit = true;
        projectilePrefab = projectile;
        attackRate = rate;
    }

    private bool isFireing; // true ������Ÿ�� �߻���,   false �߻����� �ʴ� ����. 

    public bool FIRING
    {
        set
        {
            isFireing = value;

            if (isInit) // �ʱ�ȭ�� �� ����鸸 �۵��ϵ���. 
            {
                if (isFireing)
                {
                    // ���� �۵�.
                    StartCoroutine("TryAttack");
                }
                else
                {
                    // ���� �۵� ����. 
                    StopCoroutine("TryAttack");
                }
            }
        }

        get => isFireing;
    }

    private GameObject obj;
    private IEnumerator TryAttack()
    {
        while (true)
        {
            // �����ڵ� - �ǽð� ����.
            //Instantiate(projectilePrefab, transform.position, Quaternion.identity); // ������Ÿ�� �߻�.

            // �����ڵ� - ������Ʈ Ǯ�� �̿��Ͽ� ������Ʈ�� ��Ȱ���ϴ� �ڵ�
            obj = ObjectPool_Manager.Inst.pools[(int)ObjectType.ObjT_Projectile_01].PopObj();
            obj.transform.position = transform.position;



            yield return new WaitForSeconds(attackRate); // ���� ������. 
        }
    }

}
