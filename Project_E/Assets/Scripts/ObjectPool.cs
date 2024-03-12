using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // ����
    public static ObjectPool poolInstance;

    // ����Ҫ�洢������
    public GameObject Object;
    // �ڴ��������У�
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    // ���ӵĳ�ʼ����
    public int defaultCount = 16;
    // ���ӵ��������
    public int maxCount = 25;

    private void Awake()
    {
        poolInstance = this;
    }

    // �Գ��ӽ��г�ʼ����������ʼ�������������壩
    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < defaultCount; i++)
        {
            obj = Instantiate(Object, this.transform);
            // �����ɵĶ������
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    // �ӳ�����ȡ������
    public GameObject Get()
    {
        GameObject tmp;
        // ��������������壬�ӳ���ȡ��һ������
        if (objectPool.Count > 0)
        {
            // ���������
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        // ���������û�����壬ֱ���½�һ������
        else
        {
            tmp = Instantiate(Object, this.transform);
        }
        return tmp;
    }
    // ��������ս�����
    public void Remove(GameObject obj)
    {
        // �����е�������Ŀ�������������
        if (objectPool.Count <= maxCount)
        {
            // �ö���û���ڶ�����
            if (!objectPool.Contains(obj))
            {
                // ���������
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        // �����������������
        else
        {
            Destroy(obj);
        }
    }
}
