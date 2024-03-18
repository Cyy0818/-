using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.Translate( Time.deltaTime * speed *Vector3.forward);
    }
}
