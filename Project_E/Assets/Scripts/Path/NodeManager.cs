using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path;
public class NodeManager : MonoBehaviour
{
    public Node StartNode;
    public Node TargetNode;
    public Node[,] MapNodes;
    public List<Node> Path;

    public List<Node> FindPath()
    {
        Path = PathFinding.FindPath(StartNode,TargetNode,MapNodes);
        return Path;
    }
    

}