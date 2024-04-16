
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Path
{
    public class PathFinding
    {
        public static List<Node> FindPath(Node start, Node target, Node[,] nodes )
        {
            //清楚邻居节点
            ClearNodes(nodes);
            var toSearch = new List<Node> {start};
            var processed = new List<Node>();
            
            //
    
            while (toSearch.Any())//当toSearch为空时，没有道路
            {
                var current = toSearch[0];
                foreach (var tNode in toSearch)
                {
                    if (tNode.F < current.F || tNode.F == current.F && tNode.H < current.H)
                    {
                        current = tNode;
                    }
                }
                
                processed.Add(current);
                toSearch.Remove(current);
    
                //找到
                if (current == target)
                {
                    var curPathNode = target;
                    var path = new List<Node>();
                    while (curPathNode != start)
                    {
                        path.Add(curPathNode);
                        curPathNode = curPathNode.Connection;
                    }

                    path.Reverse();
                    //PrintPath(path);
                    return path;
                }
                //Debug.Log("CurNode:" + current.X + "," + current.Y);
                //添加Neighbor
                current.Neighbors ??= new List<Node>();
                if(current.X-1 >= 0) current.Neighbors.Add(nodes[current.X-1,current.Y]);
                if(current.X+1 < nodes.GetLength(0)) current.Neighbors.Add(nodes[current.X+1,current.Y]);
                if(current.Y-1 >= 0) current.Neighbors.Add(nodes[current.X,current.Y-1]);
                if(current.Y+1 < nodes.GetLength(1)) current.Neighbors.Add(nodes[current.X,current.Y+1]);
                
                foreach (var neighbor in current.Neighbors.Where(n => !processed.Contains(n)))
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetManhattanDistance(neighbor) * neighbor.Weight;
                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG((int)costToNeighbor);
                        neighbor.SetConnnetion(current);
                        if (!inSearch)
                        {
                            neighbor.SetH(neighbor.GetDistance(target)) ;
                            toSearch.Add(neighbor);
                        } 
                    }
                }
            }
    
            return null;
        }

        private static void PrintPath(List<Node> path)
        {
            foreach (var node in path)
            {
                Debug.Log("X: " + node.X + "Y: " + node.Y);
            }
        }

        public static void ClearNodes(Node[,] nodes)
        {
            foreach (var node in nodes)
            {
                node.Neighbors?.Clear();
            }
        }
    }

}
