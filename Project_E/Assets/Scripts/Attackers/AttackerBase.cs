using System;
using System.Collections.Generic;
using UnityEngine;
using Path;
namespace Attacker
{
    public class AttackerBase : MonoBehaviour 
    {
        private Rigidbody _rb;
        [Header("寻路")] 
        //应该记录自身所在的节点位置
        [SerializeField]private int _curNode = 0;//当前节点
        [SerializeField]private List<Node> path;
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
            NodeManager.PathUpdate += SetPath;//订阅到NodeManager
            //初始化路径
            path = NodeManager.Path;
        }

        private void OnDestroy()
        {
            NodeManager.PathUpdate -= SetPath;//取消订阅
            WaveManager.EnemiesAliveCounter--;
            WaveManager.TotalAttackerCounter--;
            Debug.Log("剩余Attackers:" +  WaveManager.TotalAttackerCounter);
        }

        private void SetPath(Node[,] maps)
        {
            var start = path[_curNode];
            var target = path[^1];
            if(start == target) ReachDestination();
            _curNode = 0;
            path = PathFinding.FindPath(start, target, maps);
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
            var targetPosition = new Vector3(path[_curNode].transform.position.x, path[_curNode].transform.position.y + 1, path[_curNode].transform.position.z);
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
                    ReachDestination();
                }
            }
        }
        void ReachDestination()
        {
            //GameObject.Destroy(this.gameObject);
            _rb.velocity = Vector3.zero;
            Dead();
        }
        public void TakeDamage(float attack)
        {
            health -= attack;
        }

        private void Dead()
        {
            Debug.Log("dead");
            Destroy(gameObject);
            
        }
      
    }

    
}