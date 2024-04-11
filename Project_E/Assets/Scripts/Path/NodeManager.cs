using System.IO;
using UnityEngine;
using Path;
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
    public int[,] _matrix;
    
    [Header("节点信息")]
    private Node _startNode;
    private Node _targetNode;
    private Node[,] _mapNodes;

    [Header("WaveManager")] 
    public WaveManager waveManager;

    private void Start()
    {
        _matrix = ReadMatrixFormFile(matrixFilePath);
        //行反转
        _matrix = ReverseMatrixRows(_matrix);
        _mapNodes = new Node[_matrix.GetLength(1),_matrix.GetLength(0)];
        GenerateMap(_matrix);
        //开始生成敌人
        StartAttackerGenerate();
    }

    #region MapGenerator

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
    
    private Node SetNode(GameObject block,int x, int y,float weight)
    {
        var node = block.AddComponent<Node>();
        node.X = x;
        node.Y = y;
        node.Weight = weight;
        _mapNodes[x, y] = node;
        return node;
    }
    
    #endregion

    #region Start

    private void StartAttackerGenerate()
    {
        var startPosition = _startNode.transform;
        waveManager.Init(startPosition,_startNode,_targetNode,_mapNodes);
        waveManager.EnemyGenerate();
    }

    #endregion
    // 打印地图的权重矩阵
    public void PrintMatrix()
    {
        Debug.Log("Map Matrix:");
        int rows = _matrix.GetLength(0);
        int columns = _matrix.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            string rowString = "";
            for (int col = 0; col < columns; col++)
            {
                rowString += _matrix[row, col] + " ";
            }
            Debug.Log(rowString);
        }
    }
}