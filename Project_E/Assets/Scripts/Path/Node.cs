using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public class Node : MonoBehaviour
    {
        public int X;
        public int Y;
        public int G { get; private set; } //实际代价
        public int H { get; private set; } //估算代价
        public int F => G + H; //代价总和
        public Node Connection { get; private set; } //前置节点

        public List<Node> Neighbors; //相邻的节点

        public float Weight; //矩阵权重

        public void SetG(int G)
        {
            this.G = G;
        }

        public void SetH(int H)
        {
            this.H = H;
        }

        public void SetConnnetion(Node Connection)
        {
            this.Connection = Connection;
        }

        public int GetDistance(Node other)
        {
            var dx = Mathf.Abs(X - other.X);
            var dy = Mathf.Abs(Y - other.Y);
            return dx + dy;
        }
    }
}