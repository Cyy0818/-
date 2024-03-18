using System.Collections.Generic;
using System.Linq;

namespace Path
{
    public class PathFinding
    {
        public static List<Node> FindPath(Node start, Node target, Node[,] nodes)
        {
            var toSearch = new List<Node> {start};
            var processed = new List<Node>();
    
            while (toSearch.Any())//当toSearch为空时，没有道路
            {
                var current = toSearch[0];
                foreach (var tNode in toSearch)
                {
                    if (tNode.F < current.F || tNode.F == current.F && tNode.F < current.F)
                    {
                        current = tNode;
                    }
                }
                
                processed.Add(current);
                toSearch.Remove(current);
    
                if (current == target)
                {
                    var curPathNode = target;
                    var path = new List<Node>();
                    while (curPathNode != start)
                    {
                        path.Add(curPathNode);
                        curPathNode = curPathNode.Connection;
                    }
    
                    return path;
                }
    
                //添加Neighbor
                if(nodes[current.X-1,current.Y] != null) current.Neighbors.Add(nodes[current.X-1,current.Y]);
                if(nodes[current.X+1,current.Y] != null) current.Neighbors.Add(nodes[current.X+1,current.Y]);
                if(nodes[current.X,current.Y-1] != null) current.Neighbors.Add(nodes[current.X,current.Y-1]);
                if(nodes[current.X,current.Y+1] != null) current.Neighbors.Add(nodes[current.X,current.Y+1]);
                
                foreach (var neighbor in current.Neighbors.Where(n=>n.Type == E_Node_Type.Ground && !processed.Contains(n)))
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetDistance(neighbor);
                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG((int)costToNeighbor);
                        neighbor.SetConnnetion(current);
                        if (!inSearch)
                        {
                            neighbor.SetH((int)neighbor.GetDistance(target));
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
    
            return null;
        }
        
    }

}



