using System.Collections.Generic;
using UnityEngine;
using Path;
namespace Attacker
{
    public class AttackerBase : MonoBehaviour 
    {
        private Rigidbody _rb;
        private int _curNode = 0;//当前节点
        [Header("寻路")] 
        private Node _startNode;
        private Node _targetNode;
        private Node[,] _mapNodes;
        public float passWeight = 1f;
        public List<Node> path;
        public float arrivalRange = 0.3f;//检测范围阈值
        [Header("属性面板")]
        public float Health;
        public float ATK;
        public float Speed;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            path = PathFinding.FindPath(_startNode, _targetNode, _mapNodes, passWeight);
        }

        private void Update()
        {
            if (Health <= 0)
            {
                Dead();
            }
        }

        private void FixedUpdate()
        {
            MovePosition();
        }

        private void MovePosition()
        {
            var targetPosition = new Vector3(path[_curNode].transform.position.x, path[_curNode].transform.position.y, -1);
            //Debug.Log("AttackerPostionX:" + transform.position.x + "AttackerPostionY:" + transform.position.y);
            //Debug.Log("TargetPositionX: " + _path[_curNode].transform.position.x + "TargetPositionY: " + _path[_curNode].transform.position.y);
            var distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > arrivalRange)
            {
                var direction = (targetPosition - transform.position).normalized;
                var movement = direction * Speed;
                //_rb.MovePosition(targetPosition + movement);
                _rb.velocity = movement;
            }
            else
            {
                if (_curNode < path.Count - 1)
                {
                    _curNode++;
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }
        }

        public void SetNode(Node start, Node target, Node[,] mapNodes)
        {
            _startNode = start;
            _targetNode = target;
            _mapNodes = mapNodes;
        }
        public void FindPath()
        {
            _startNode = path[_curNode];
            path = PathFinding.FindPath(_startNode, _targetNode, _mapNodes, passWeight);
        }
        public void BeHurt(float attack)
        {
            Health -= attack;
        }

        private void OnDestroy()
        {
            WaveManager.EnemiesAliveCounter--;
        }

        private void Dead()
        {
            GameObject.Destroy(this.gameObject);
        }
        public void GetDamage(int damage)
        {
            
        }
    }

    
}