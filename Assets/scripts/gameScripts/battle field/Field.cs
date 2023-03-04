using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    //Скрипт для создания игрового поля
    [SerializeField] private int width, height;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private GameObject mountainPrefab;
    private Dictionary<Vector2, Cell > Cells;
    public Cell[,] CellsOfFieled = new Cell[7, 11];
    private void Awake()
    {
        GenerateField();
    }

    void GenerateField()
    {
        Cells = new Dictionary<Vector2, Cell>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell spawnedTile = Instantiate(cellPrefab, Vector3.zero,Quaternion.identity,transform);
                spawnedTile.name = $"Cell {i} {j}";
                spawnedTile.transform.localPosition = new Vector3(j * -0.27f,0, i * -0.27f);
                spawnedTile.Enabled = true;
                if ((j==4 || j==6)&&i%2!=0)
                {
                    GameObject Mountain = Instantiate(mountainPrefab, Vector3.zero, Quaternion.identity, spawnedTile.transform);
                    Mountain.transform.localPosition = new Vector3(0, 0.5f, 0);
                }
                //Ессли чётная, то один цвет, нечетная - другой
                if ((i + j) % 2 == 1)
                {
                    spawnedTile.Init(true);
                }
                else
                {
                    spawnedTile.Init(false);
                }
                //Запись клеток в списки
                Cells[new Vector2(spawnedTile.transform.position.x, spawnedTile.transform.position.z)] = spawnedTile;
                CellsOfFieled[i,j] = spawnedTile;
            }
        }
    }

    //Для получниея координат клетки
    public Cell GetCellAtPosition(Vector2 pos)
    {
        if (Cells.TryGetValue(pos, out var tile))
        {
            Debug.Log(tile + " " + pos);
            return tile;
        }
        return null;
    }
}
