using UnityEngine;
using System.IO;
using Path;

public class MapGenerator : MonoBehaviour
{
    public GameObject wall;
    public GameObject ground;

    public GameObject attackerSpawn;
    
    public string matrixFilePath;
    public float size = 1.0f;
    [Header("地图节点管理")]
    public NodeManager nodeManager;

    void Start()
    {
        int[,] matrix = ReadMatrixFromFile(matrixFilePath);
        GenerateMap(matrix);
        MoveCamera2MapCenter(matrix);
    }

    int[,] ReadMatrixFromFile(string filePath)
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

    void GenerateMap(int[,] matrix)
    {

        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);
        
        //初始化节点矩阵数组
        nodeManager.MapNodes = new Node[rows, columns];
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPos = col * size;
                float yPos = row * size;

                int matrixValue = matrix[row, col];

                var position = new Vector3(xPos, yPos, 0);
                if (matrixValue == 0)
                {
                    nodeManager.MapNodes[row, col] = new Node(row, col, E_Node_Type.Wall, position);
                    Instantiate(wall, position, Quaternion.identity, transform);
                }
                else if (matrixValue == 1)
                {
                    nodeManager.MapNodes[row, col] = new Node(row, col, E_Node_Type.Ground, position);
                    Instantiate(ground, position, Quaternion.identity, transform);
                }
                else if(matrixValue == 2)
                {
                    nodeManager.MapNodes[row, col] = new Node(row, col, E_Node_Type.Ground, position);
                    Instantiate(attackerSpawn, position, Quaternion.identity, transform);
                    nodeManager.StartNode = nodeManager.MapNodes[row, col];
                }
                else if(matrixValue == 3)
                {
                    nodeManager.MapNodes[row, col] = new Node(row, col, E_Node_Type.Ground, position);
                    Instantiate(ground, position, Quaternion.identity, transform);
                    nodeManager.TargetNode = nodeManager.MapNodes[row, col];
                }
            }
        }
    }
    void MoveCamera2MapCenter(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        float chessboardCenterX = (columns - 1) * size / 2f;
        float chessboardCenterY = (rows - 1) * size / 2f;
        Camera.main.transform.position = new Vector3(chessboardCenterX, chessboardCenterY, Camera.main.transform.position.z);
    }
}
