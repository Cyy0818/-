using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Attacker;
using Path;
using UnityEngine;

public class PathTest : MonoBehaviour
{
    //0是墙，1是路
    public int[,] Matrix =
    {
        { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0 },
        { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0 },
        { 0, 0, 0, 0, 0, 0,0,1,0,0,1,0,0,0 },
        { 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1 },
        { 1, 1, 1, 1, 1, 1,1,1,1,1,1,1,1,1 },
        { 0, 0, 0, 0, 0, 0,1,0,0,1,0,0,0,0 },
        { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0 },
    };

    public GameObject start;
    public GameObject target;
    public GameObject attacker;

    private Node[,] _mapNodes;

    public Node StartNode;
    public Node TargetNode;
    
    

    private void Start()
    {
        InitNodeMatrix();
        //PathFinding.FindPath(StartNode,TargetNode,_mapNodes);
        GenerateAttacker();
    }

    private void InitNodeMatrix()
    {
        var rows = Matrix.GetLength(0);
        var cols = Matrix.GetLength(1);
        _mapNodes = new Node[rows,cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var position = new Vector3(j, rows - 1 - i, 0);
                
                if (Matrix[i, j] == 1)
                {
                    _mapNodes[i, j] = new Node(i, j, E_Node_Type.Ground, position);
                }
                else if (Matrix[i, j] == 0)
                {
                    _mapNodes[i, j] = new Node(i, j, E_Node_Type.Wall, position);
                }
            }
        }

        StartNode = _mapNodes[1, 0];
        TargetNode = _mapNodes[1, 5];
    }

    private void GenerateAttacker()
    {
        try
        {
            Debug.Log("StartPostionX:" + start.transform.position.x + "StartPostionY:" + start.transform.position.y);
            var position = new Vector3(start.transform.position.x, start.transform.position.y, -1);
           var curAttacker = Instantiate(attacker,position,Quaternion.identity);
           curAttacker.GetComponent<AttackerBase>()._path = PathFinding.FindPath(StartNode,TargetNode,_mapNodes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
