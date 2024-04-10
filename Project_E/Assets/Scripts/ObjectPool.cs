using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ObjectPool
{
    private Dictionary<string, Queue<GameObject>> _objectPool = new();
    private GameObject _pool;

    public ObjectPool(GameObject pool)
    {
        _pool = pool;
    }
    
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object; 
        if (!_objectPool.ContainsKey(prefab.name) || _objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            if (_pool == null)
            {
                _pool = new GameObject(prefab.name + "Pool");
            }
            _object.transform.SetParent(_pool.transform);
        }

        _object = _objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    public void PushObject(GameObject prefab)
    {
        var name = prefab.name.Replace("(Clone)", string.Empty);
        if (!_objectPool.ContainsKey(name))
        {
            _objectPool.Add(name,new Queue<GameObject>());
        }
        _objectPool[name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}
