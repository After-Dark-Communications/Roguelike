using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.scripts.Level_Generation;

public class LevelCreator : MonoBehaviour
{
    public int _rows;
    public int _columns;
    public Tile _wallTile;
    public Tile _floorTile;
    public Tilemap _wallTileMap;
    public Tilemap _floorTileMap;
    public int _overlappingGroupsCount = 0;

    private List<Vector3Int> _gridPositions = new List<Vector3Int>();
    private List<Room> _rooms = new List<Room>();
    private List<Room> notConnected = new List<Room>();
    private List<Room> connectedRooms = new List<Room>();
    private int[] mapBorders;
    private List<Vector3Int> availableOpenings = new List<Vector3Int>();

    void Awake()
    {
        _rooms.Clear();
        mapBorders = new int[4] { 0 - _columns / 2, 0 - _rows / 2, _columns / 2, _rows / 2 };
        InitializeGrid();
        //GenerateEmptyFloor();
        GenerateRooms(10, 4, 10);
        AddConnections();
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

    private void GenerateEmptyFloor()
    {
        GenerateWallsAndFloors(new Vector3Int(mapBorders[0], mapBorders[1], 0), mapBorders[2], mapBorders[3], true);
    }

    private void GenerateWallsAndFloors(Vector3Int pos, int columns, int rows, bool playerRoom)
    {
        Room newRoom = new Room(pos, columns, rows, playerRoom);
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
                        newRoom.roomWalls.Add(here);                /*new TerrainTile(here, TileTypes.Wall)*/
                    }
                    else
                    {
                        newRoom = UpdateRoomOverlaps(newRoom, here); 
                    }
                }
                else if (x > pos.x && x < columns && y > pos.y && y < rows)
                {
                    _floorTileMap.SetTile(here, _floorTile);
                    if (_wallTileMap.HasTile(here))
                    {
                        newRoom = UpdateRoomOverlaps(newRoom, here);
                        _wallTileMap.SetTile(here, null);
                    }
                    newRoom.roomFloors.Add(here);                   /*new TerrainTile(here, TileTypes.Floor)*/
                }
            }
        }
        _rooms.Add(newRoom);
        if (newRoom.connected)
        {
            connectedRooms.Add(newRoom);
        }
        else
        {
            notConnected.Add(newRoom);
        }
    }

    private bool CheckWallSpot(Vector3Int pos, int columns, int rows, int x, int y)
    {
        return (x <= columns && x >= pos.x && (y == rows || y == pos.y)) || (y <= rows && y >= pos.y && (x == columns || x == pos.x));
    }

    private void GenerateRooms(int rooms, int minSize, int maxSize)
    {
        // generates small room around player, probably will be changed if the player can be placed in a pre-generated room instead
        GenerateWallsAndFloors(new Vector3Int(-3, -3, 0), 3, 3, true);

        for (int i = 0; i < rooms; i++)
        {
            int roomColumns = Random.Range(minSize, maxSize + 1);
            int roomRows = Random.Range(minSize, maxSize + 1);
            Vector3Int roomPos = _gridPositions[Random.Range(0, _gridPositions.Count)];
            GenerateWallsAndFloors(roomPos, roomColumns + roomPos.x, roomRows + roomPos.y, false);
        }
    }

    private Room UpdateRoomOverlaps(Room newRoom, Vector3Int currentTile)
    {
        foreach (Room room in _rooms)
        {
            if (room.roomWalls.Contains(currentTile))
            {
                room.roomWalls.Remove(currentTile);
                room.roomFloors.Add(currentTile);
                if (room.overlappingRoomGroup == 0)
                {
                    _overlappingGroupsCount++;
                    room.overlappingRoomGroup = _overlappingGroupsCount;
                    newRoom.overlappingRoomGroup = room.overlappingRoomGroup;
                }
                else
                {
                    newRoom.overlappingRoomGroup = room.overlappingRoomGroup;
                }
            }
        }
        return newRoom;
    }

    private void AddConnections()
    {
        List<int> overlapRooms = new List<int>();
        foreach (Room room in _rooms)
        {
            if (room.overlappingRoomGroup != 0)
            {
                if (!overlapRooms.Contains(room.overlappingRoomGroup))
                {
                    overlapRooms.Add(room.overlappingRoomGroup);
                }
            }
            if (!room.connected)
            {
                Vector3Int hallwayStart = room.roomFloors[Random.Range(0, room.roomFloors.Count)];
                Vector3Int hallwayEnd = GetHallwayEndPoint();
                CreateHallway(hallwayStart, hallwayEnd);
                room.connected = true;
                notConnected.Remove(room);
                connectedRooms.Add(room);
            }
        }
    }

    private Vector3Int GetHallwayEndPoint()
    {
        Vector3Int endpoint;
        Room connectedRoom = connectedRooms[Random.Range(0, connectedRooms.Count)];
        endpoint = connectedRoom.roomFloors[Random.Range(0, connectedRoom.roomFloors.Count)];
        return endpoint;
    }

    private void CreateHallway(Vector3Int start, Vector3Int end)
    {
        int distance = ManhattanDistance(start, end);
        Vector3Int currentPoint = start;
        ReplaceWallWithFloor(currentPoint);
        while (distance > 0)
        {
            for (int dir = 0; dir < 8; dir++)
            {
                FillEmptyWithWall(GetSurroundingTile(dir, currentPoint));
            }
            currentPoint = GetNextTile(currentPoint, end, distance);
            ReplaceWallWithFloor(currentPoint);
            distance = ManhattanDistance(currentPoint, end);
        }
    }

    private Vector3Int GetNextTile(Vector3Int currentPoint, Vector3Int endPoint, int distance)
    {
        if (Random.value < 0.5f)
        {
            for (int direction = 0; direction < 4; direction++)
            {
                if (ManhattanDistance(GetCardinalTile(direction, currentPoint), endPoint) < distance)
                {
                    return GetCardinalTile(direction, currentPoint);
                }
            }
        }
        else
        {
            for (int direction = 3; direction > -1; direction--)
            {
                if (ManhattanDistance(GetCardinalTile(direction, currentPoint), endPoint) < distance)
                {
                    return GetCardinalTile(direction, currentPoint);
                }
            }
        }
        return (currentPoint);
    }

    private Vector3Int GetSurroundingTile(int dirNumber, Vector3Int basePoint)
    {
        switch (dirNumber)
        {
            case 0:
                return new Vector3Int(basePoint.x, basePoint.y + 1, 0);
            case 1:
                return new Vector3Int(basePoint.x + 1, basePoint.y + 1, 0);
            case 2:
                return new Vector3Int(basePoint.x + 1, basePoint.y, 0);
            case 3:
                return new Vector3Int(basePoint.x + 1, basePoint.y - 1, 0);
            case 4:
                return new Vector3Int(basePoint.x, basePoint.y - 1, 0);
            case 5:
                return new Vector3Int(basePoint.x - 1, basePoint.y - 1, 0);
            case 6:
                return new Vector3Int(basePoint.x - 1, basePoint.y, 0);
            case 7:
                return new Vector3Int(basePoint.x - 1, basePoint.y + 1, 0);
            default:
                return basePoint;
        }
    }
    private Vector3Int GetCardinalTile(int dirNumber, Vector3Int basePoint)
    {
        switch (dirNumber)
        {
            case 0:
                return new Vector3Int(basePoint.x, basePoint.y + 1, 0);
            case 1:
                return new Vector3Int(basePoint.x + 1, basePoint.y, 0);
            case 2:
                return new Vector3Int(basePoint.x, basePoint.y - 1, 0);
            case 3:
                return new Vector3Int(basePoint.x - 1, basePoint.y, 0);
            default:
                return basePoint;
        }
    }

    private int ManhattanDistance(Vector3Int a, Vector3Int b)
    {
        checked
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }
    }

    private void ReplaceWallWithFloor(Vector3Int tilePosition)
    {
        _wallTileMap.SetTile(tilePosition, null);
        _floorTileMap.SetTile(tilePosition, _floorTile);
    }

    private void FillEmptyWithWall(Vector3Int tilePosition)
    {
        if (!_floorTileMap.HasTile(tilePosition))
        {
            _wallTileMap.SetTile(tilePosition, _wallTile);
        }
    }
}
