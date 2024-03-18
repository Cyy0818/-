using System.Collections.Generic;
using UnityEngine;
using Path;
namespace Attacker
{
    public class AttackerBase : MonoBehaviour 
    {
        private AttackerSpawn _spawn;
        private Rigidbody2D _rb;
        private int curNode = 0;
        public List<Node> _path;
        public float Health;
        public float ATK;
        public float Speed;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            MovePosition();
        }

        private void Dead()
        {
            Destroy(transform);    
        }
        
        private void MovePosition()
        {
            var targetPosition = _path[curNode].Position;
            if (transform.position != targetPosition)
            {
                var direction = (targetPosition - transform.position).normalized;
                var movement = direction * (Speed * Time.fixedDeltaTime);
                _rb.MovePosition(targetPosition+movement);
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
                    _rb.angularVelocity = 0;
                }
            }
        }

        public void GetDamage(int damage)
        {
            
        }
    }
    
}