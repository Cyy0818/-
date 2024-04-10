using System.Collections.Generic;
using UnityEngine;
using Path;
namespace Attacker
{
    public class AttackerBase : MonoBehaviour 
    {
        private Rigidbody _rb;
        [SerializeField] [Header("管理器")] private WaveManager _waveManager;
        [Header("寻路")] 
        //应该记录自身所在的节点位置
        private int _curNode = 0;//当前节点
        private Node _startNode;
        private Node _targetNode;
        private Node[,] _mapNodes;
        public float passWeight = 1f;
        public List<Node> path;
        public float arrivalRange = 0.3f;//检测范围阈值
        [Header("属性面板")]
        public float health;
        public float attack;
        public float attackSpeed;
        public float speed;
        public float finialAttack;//对终点的伤害

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _waveManager = GetComponentInParent<WaveManager>();
            path = PathFinding.FindPath(_startNode, _targetNode, _mapNodes, passWeight);
        }

        private void Update()
        {
            if (health <= 0)
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
            var targetPosition = new Vector3(path[_curNode].transform.position.x, path[_curNode].transform.position.y + 1, 3);
            //Debug.Log("AttackerPostionX:" + transform.position.x + "AttackerPostionY:" + transform.position.y + "AttackerPostionZ:" + transform.position.z);
            //Debug.Log("TargetPositionX: " + targetPosition.x + "TargetPositionY: " + targetPosition.y + "TargetPositionZ: " + targetPosition.z);
            var distance = Vector3.Distance(transform.position, targetPosition);
            //Debug.Log("Distance:" + distance);
            if (distance > arrivalRange)
            {
                var direction = (targetPosition - transform.position).normalized;
                var movement = direction * speed;
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
                    //到达终点
                    _rb.velocity = Vector3.zero;
                    ReachDestination();
                }
            }
        }

        public void SetNode(Node start, Node target, Node[,] mapNodes)
        {
            _startNode = start;
            _targetNode = target;
            _mapNodes = mapNodes;
        }
        void ReachDestination()
        {
            //GameObject.Destroy(this.gameObject);
            Dead();
        }
        public void FindPath()
        {
            _startNode = path[_curNode];
            path = PathFinding.FindPath(_startNode, _targetNode, _mapNodes, passWeight);
        }
        public void TakeDamage(float attack)
        {
            health -= attack;
        }

        private void OnDestroy()
        {
            WaveManager.EnemiesAliveCounter--;
        }

        private void Dead()
        {
            //GameObject.Destroy(this.gameObject);
            _waveManager._objectPool.PushObject(gameObject);
            _waveManager.totalAttackerCounter--;
        }
      
    }

    
}