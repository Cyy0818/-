using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject wall;
    public GameObject ground;
    public string matrixFilePath;
    public float size = 1.0f;

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

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPos = col * size;
                float yPos = row * size;

                int matrixValue = matrix[row, col];

                if (matrixValue == 0)
                {
                    Instantiate(wall, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
                }
                else if (matrixValue == 1)
                {
                    Instantiate(ground, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
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
