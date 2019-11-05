using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCreator : MonoBehaviour
{
    public int _rows;
    public int _columns;
    public Tile _wallTile;
    public Tile _floorTile;
    public Tilemap _wallTileMap;
    public Tilemap _floorTileMap;

    private List<Vector3Int> _gridPositions = new List<Vector3Int>();
    private int[] mapBorders;

    void Awake()
    {
        mapBorders = new int[4]{ 0 - _columns / 2, 0 - _rows / 2, _columns / 2, _rows / 2 };
        InitializeGrid();
        //GenerateEmptyFloor();
        GenerateRooms(15, 4, 8);
    }

    private void InitializeGrid()
    {
        _gridPositions.Clear();
        for (int x = mapBorders[0]; x < mapBorders[2]; x++)
        {
            for (int y = mapBorders[1]; y < mapBorders[3]; y++)
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
        GenerateWallsAndFloors(new Vector3Int(mapBorders[0], mapBorders[1], 0), mapBorders[2], mapBorders[3]);
    }

    private void GenerateWallsAndFloors(Vector3Int pos, int columns, int rows)
    {
        for (int x = pos.x; x < columns + 1; x++)
        {
            for (int y = pos.y; y < rows + 1; y++)
            {
                Vector3Int here = new Vector3Int(x, y, 0);
                if (CheckWallSpot(pos, columns, rows, x, y))
                {
                    if (!_floorTileMap.HasTile(here))
                    {
                        _wallTileMap.SetTile(here, _wallTile);
                    }
                }
                else if (x > pos.x && x < columns && y > pos.y && y < rows)
                {
                    _floorTileMap.SetTile(here, _floorTile);
                    if (_wallTileMap.HasTile(here))
                    {
                        _wallTileMap.SetTile(here, null);
                    }
                }
            }
        }
    }

    private bool CheckWallSpot(Vector3Int pos, int columns, int rows, int x, int y)
    {
        return (x <= columns && x >= pos.x && (y == rows || y == pos.y)) || (y <= rows && y >= pos.y && (x == columns || x == pos.x));
    }

    private void GenerateRooms(int rooms, int minSize, int maxSize)
    {
        for (int i = 0; i < rooms; i++)
        {
            int roomColumns = Random.Range(minSize, maxSize + 1);
            int roomRows = Random.Range(minSize, maxSize + 1);
            Vector3Int roomPos = _gridPositions[Random.Range(0, _gridPositions.Count)];
            GenerateWallsAndFloors(roomPos, roomColumns + roomPos.x, roomRows + roomPos.y);
        }
    }
}
