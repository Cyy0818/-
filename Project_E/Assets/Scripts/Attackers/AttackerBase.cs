using System.Collections.Generic;
using UnityEngine;
using Path;
namespace Attacker
{
    public class AttackerBase : MonoBehaviour 
    {
        private AttackerSpawn _spawn;
        private Rigidbody _rb;
        private int curNode = 0;
        public List<Node> _path;
        public float Health;
        public float ATK;
        public float Speed;

        private float _runTimer = 0.1f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void BeHurt(float attack)
        {
            Health -= attack;
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
            if (_runTimer <= 0)
            {
                MovePosition();
                _runTimer = 0.1f;
            }
            else
            {
                _runTimer -= Time.fixedDeltaTime;
            }
        }

        private void Dead()
        {
            Destroy(transform);    
        }
        
        private void MovePosition()
        {
            var targetPosition = new Vector3(_path[curNode].Position.x, _path[curNode].Position.y, -1);
            Debug.Log("AttackerPostionX:" + transform.position.x + "AttackerPostionY:" + transform.position.y);
            Debug.Log("TargetPositionX: " + _path[curNode].Position.x + "TargetPositionY: " + _path[curNode].Position.y);
            var arrivalRange = 0.3f;
            var distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > arrivalRange)
            {
                var direction = (targetPosition - transform.position).normalized;
                var movement = direction * (Speed * Time.fixedDeltaTime);
                _rb.MovePosition(targetPosition + movement);
            }
            else
            {
                if (curNode < _path.Count - 1)
                {
                    curNode++;
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }
        }


        public void GetDamage(int damage)
        {
            
        }
    }
    
}