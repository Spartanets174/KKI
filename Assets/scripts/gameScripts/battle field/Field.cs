using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Cell cellPrefab;
    private Dictionary<Vector2, Cell > Cells;
    private void Start()
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
                if ((i + j) % 2 == 1)
                {
                    spawnedTile.Init(true);
                }
                else
                {
                    spawnedTile.Init(false);
                }
                Cells[new Vector2(width,height)] = spawnedTile;
                
            }
        }
    }

    public Cell GetCellAtPosition(Vector2 pos)
    {
        if (Cells.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
