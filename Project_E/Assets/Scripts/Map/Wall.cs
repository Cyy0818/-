using System;
using Path;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject groundPrefab;
    public float health = 30f;
    private Node _node;

    private void Start()
    {
        _node = GetComponent<Node>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            TurnGround();
        }
    }

    public void TurnGround()
    {
        Destroy(gameObject);
        var ground = GameObject.Instantiate(groundPrefab, transform.position, Quaternion.identity);
        NodeManager.WallTurnToGround(ground, _node.X, _node.Y);
    }
    
    public void TakeDamage(float attack)
    {
        health -= attack;
    }
}
