using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Path
{
    public class Node : MonoBehaviour
    {
        public int X;
        public int Y;
        public float G; //实际代价
        public float H;//估算代价
        public float F => G + H; //代价总和
        public Node Connection { get; private set; } //前置节点

        public List<Node> Neighbors; //相邻的节点

        public float Weight; //矩阵权重

        public void SetG(float G)
        {
            this.G = G;
        }

        public void SetH(float H)
        {
            this.H = H;
        }

        public void SetConnnetion(Node Connection)
        {
            this.Connection = Connection;
        }

        public float GetDistance(Node other)
        {
            var dx = Mathf.Pow(X - other.X, 2);
            var dy = Mathf.Pow(Y - other.Y, 2);
            return Mathf.Sqrt(dx + dy);
        }
        
        public float GetManhattanDistance(Node other)
        {
            var dx = Mathf.Abs(X - other.X);
            var dy = Mathf.Abs(Y - other.Y);
            return dx + dy;
        }

        public void IncreaseWeight(float weight)
        {
            this.Weight += weight;
        }

        public void ReduceWeight(float weight)
        {
            this.Weight -= weight;
        }
    }
}