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
    private int[,] _matrix;
    
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
        MoveCamera2MapCenter(_matrix);
        
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

    
    void MoveCamera2MapCenter(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        
        // 计算棋盘中心位置
        float chessboardCenterX = (columns - 1) * size / 2f;
        float chessboardCenterZ = (rows - 1) * size / 2f;

        // 设置摄像机的位置为棋盘斜上方 45 度的位置
        float cameraHeight = 10f; // 设置摄像机的高度
        float cameraDistance = 10f; // 设置摄像机与棋盘中心的距离
        Camera.main.transform.position = new Vector3(chessboardCenterX, cameraHeight, chessboardCenterZ - cameraDistance);

        // 设置摄像机的旋转角度
        float cameraRotationX = 45f; // 摄像机绕X轴旋转的角度
        Camera.main.transform.rotation = Quaternion.Euler(cameraRotationX, 0, 0);
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
    
}