using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        // �θ� ������Ʈ�� �����ϴ� ���
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject); // �ֻ��� �θ� ���� ����Ǵ��� ���� ���� �ʵ���,
        }
        else // �ڱ� �ڽ��� root�� ���
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
