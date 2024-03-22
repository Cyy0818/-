using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public enum E_Node_Type
    {
        Wall,Ground,
    }
    public class Node
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int G { get; private set; }//实际代价
        public int H { get; private set; } //估算代价
        public int F => G + H;//代价总和
        public Node Connection { get; private set; }//前置节点

        public Vector3 Position { get; private set; }

        public List<Node> Neighbors;//相邻的节点
    
        public E_Node_Type Type { get; private set; }
    
        public Node(int x, int y, E_Node_Type type, Vector3 position)
        {
            X = x;
            Y = y;
            Type = type;
            Position = position;
            //Debug.Log("X:" + X + "Y:" + Y + "Type:" + Type);
        }

        public void SetG(int G)
        {
            this.G = G;
        }

        public void SetH(int H)
        {
            this.H = H;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
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