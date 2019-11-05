using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCreator : MonoBehaviour
{
    public Tile _wallTile;
    public Tilemap _wallTileMap;
    //public Tile _floorTile;

    private int _rows;
    private int _columns;
    private List<Vector3Int> _gridPositions = new List<Vector3Int>();

    void Awake()
    {
        _rows = _wallTileMap.size.y;
        _columns = _wallTileMap.size.x;
        Debug.Log($"rows: {_rows}...... columns: {_columns}......");
        InitializeGrid();
        GenerateEmptyFloor();
        //GenerateRooms(20, 3, 7);
    }

    private void InitializeGrid()
    {
        _gridPositions.Clear();
        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                _gridPositions.Add(new Vector3Int(x, y, 0));
            }
        }
    }
    /// <summary>
    /// fills the entire level with floor tiles except the very edges, creating an empty level.
    /// </summary>
    private void GenerateEmptyFloor()
    {
        GenerateWallsAndFloors(new Vector3Int(0, 0, 0), _columns, _rows);
    }

    private void GenerateWallsAndFloors(Vector3Int pos, int columns, int rows)
    {
        for (int x = 0; x < columns + 1; x++)
        {
            for (int y = 0; y < rows + 1; y++)
            {
                //GameObject newTile = _floorTile;
                if (x == 0 || x == columns || y == 0 || y == rows)
                {
                    _gridPositions.Remove(new Vector3Int(x, y, 0));
                    _wallTileMap.SetTile(new Vector3Int(x, y, 0), _wallTile);
                }
            }
        }
    }

    private void GenerateRooms(int rooms, int minSize, int maxSize)
    {
        for (int i = 0; i < rooms; i++)
        {
            int roomColumns = Random.Range(minSize, maxSize + 1);
            int roomRows = Random.Range(minSize, maxSize + 1);
            Vector3Int roomPos = _gridPositions[Random.Range(0, _gridPositions.Count)];
            GenerateWallsAndFloors(roomPos, roomColumns, roomRows);
        }
    }
}
