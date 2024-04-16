using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Path;
using static Path.PathFinding;

public class NodeManager : MonoBehaviour
{
    [Header("场景Prefab")] 
    public GameObject wall;
    public GameObject ground;
    public GameObject start;
    public GameObject end;
    public float size = 1.0f;

    [Header("矩阵路径")] 
    public string matrixFilePath;
    private int[,] _matrix;
    
    [Header("节点信息")]
    private static Node _startNode;
    private static Node _targetNode;
    private static Node[,] _mapNodes;
    public static List<Node> _path;

    #region 地图生成和基本信息

        public void GenerateNodes()
    {
        _matrix = ReadMatrixFormFile(matrixFilePath);
        //行反转
        _matrix = ReverseMatrixRows(_matrix);
        _mapNodes = new Node[_matrix.GetLength(1),_matrix.GetLength(0)];
        GenerateMap(_matrix);
        MoveCamera2MapCenter(_matrix);
    }
    

    private int[,] ReadMatrixFormFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        int rows = lines.Length;
        int columns = lines[0].Split(' ').Length;
        int[,] matrix = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Split(' ');
            for (int j = 0; j < columns; j++)
            {
                int.TryParse(values[j], out matrix[i, j]);
            }
        }

        return matrix;
    }

    private int[,] ReverseMatrixRows(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        for (var i = 0; i < rows / 2; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                (matrix[i, j], matrix[rows - 1 - i, j]) = (matrix[rows - 1 - i, j], matrix[i, j]);
            }
        }

        return matrix;
    }
    
    private void GenerateMap(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPos = col * size;
                float yPos = 0; // 设置为 0，因为我们在 XZ 平面上搭建地图
                float zPos = row * size;

                int matrixValue = matrix[row, col];
                var position = new Vector3(xPos, yPos, zPos);
                var block = ground;
                /*
                 * 0-起点
                 * -1-终点
                 * 1-路
                 * 2-墙
                 */
                if (matrixValue == 0)
                {
                    block = start;
                }
                else if (matrixValue == 1)
                {
                    block = ground;
                }
                else if (matrixValue == 2)
                {
                    block = wall;
                }
                else if (matrixValue == -1)
                {
                    block = end;
                }
                // 添加Node组件，设定起点终点
                block = Instantiate(block, position, Quaternion.identity, transform);
                var node = SetNode(block, col, row, matrixValue);
                if (matrixValue == 0)
                {
                    _startNode = node;
                }
                else if (matrixValue == -1)
                {
                    _targetNode = node;
                }
            }
        }
    }

    
    void MoveCamera2MapCenter(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        
        // 计算棋盘中心位置
        float chessboardCenterX = (columns - 1) * size / 2f;
        float chessboardCenterZ = (rows - 1) * size / 2f;

        // 设置摄像机的位置为棋盘斜上方 45 度的位置
        float cameraHeight = 7f; // 设置摄像机的高度
        float cameraDistance = 10f; // 设置摄像机与棋盘中心的距离
        Camera.main.transform.position = new Vector3(chessboardCenterX, cameraHeight, 0.5f);

        // 设置摄像机的旋转角度
        float cameraRotationX = 70f;// 摄像机绕X轴旋转的角度
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }



    private Node SetNode(GameObject block,int x, int y,float weight)
    {
        var node = block.AddComponent<Node>();
        node.X = x;
        node.Y = y;
        node.Weight = weight <= 0 ? 0 : weight;
        _mapNodes[x, y] = node;
        return node;
    }

    public Node GetStartNode()
    {
        return _startNode;
    }

    public Node GetTargetNode()
    {
        return _targetNode;
    }

    public Node[,] GetMapNodes()
    {
        return _mapNodes;
    }

    #endregion

    #region 寻路管理

    public static event Action<List<Node>> PathUpdate; 
    public void InitPath()
    {
        _path = FindPath(_startNode, _targetNode, _mapNodes);
    }
    
    /// <summary>
    /// 当权重发生改变时，修改路径
    /// </summary>
    private static void TransmitPath()
    {
        //将修改后的地图矩阵重新进行寻路，再使用PathUpdate广播给所有活着的敌人
        _path = FindPath(_startNode, _targetNode, _mapNodes);
        PathUpdate?.Invoke(_path);
    }
    
    #endregion

    #region 权重管理
    
/// <summary>
/// 增加塔以及攻击范围的权重
/// </summary>
/// <param name="x">Node的x</param>
/// <param name="y">Node的y</param>
/// <param name="weightNode">塔的权重</param>
/// <param name="radius">攻击范围半径</param>
/// <param name="weightRadius">攻击范围权重修改</param>
    public static void Increase(int x, int y, float weightNode, int radius,float weightRadius)
    {
        _mapNodes[x,y].IncreaseWeight(weightNode);
        for (var i = x - radius; i <= x + radius; i++)
        {
            for (var j = y - radius; j <= y + radius; j++)
            {
                if (i >= 0 && i < _mapNodes.GetLength(0)
                           && j >= 0 && j < _mapNodes.GetLength(1))
                {
                    _mapNodes[i, j].IncreaseWeight(weightRadius);
                }
            }
        }
        TransmitPath();
    }

/// <summary>
/// 减少塔以及攻击范围的权重
/// </summary>
/// <param name="x">Node的x</param>
/// <param name="y">Node的y</param>
/// <param name="weightNode">塔的权重</param>
/// <param name="radius">攻击范围半径</param>
/// <param name="weightRadius">攻击范围权重修改</param>
public static void Reduce(int x, int y, float weightNode, int radius,float weightRadius)
{
    _mapNodes[x,y].ReduceWeight(weightNode);
    for (var i = x - radius; i <= x + radius; i++)
    {
        for (var j = y - radius; j <= y + radius; j++)
        {
            if (i >= 0 && i < _mapNodes.GetLength(0)
                       && j >= 0 && j < _mapNodes.GetLength(1))
            {
                _mapNodes[i, j].ReduceWeight(weightRadius);
            }
        }
    }
    TransmitPath();
}

    #endregion

}