using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.scripts.Level_Generation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private int _Rows;
    [SerializeField] private int _Columns;
    [SerializeField] private Tile _WallTile;
    [SerializeField] private Tile _FloorTile;
    [SerializeField] private Tilemap _WallTileMap;
    [SerializeField] private Tilemap _FloorTileMap;
    [Tooltip("X:How many rooms |Y: Min size |Z: max size")]
    [SerializeField] private Vector3Int _RoomSettings;
    [SerializeField] private GameObject _Door;

    private List<Vector3Int> _GridPositions = new List<Vector3Int>();
    private List<Room> _Rooms = new List<Room>();
    private List<Room> _NotConnected = new List<Room>();
    private int _OverlappingGroupsCount = 0;
    private List<Room> _ConnectedRooms = new List<Room>();
    private int[] _MapBorders;
    private List<Vector3Int> _AvailableOpenings = new List<Vector3Int>();

    void Awake()
    {
        _Rooms.Clear();
        _MapBorders = new int[4] { 0 - _Columns / 2, 0 - _Rows / 2, _Columns / 2, _Rows / 2 };
        InitializeGrid();
        //GenerateEmptyFloor();
        GenerateRooms(_RoomSettings.x, _RoomSettings.y, _RoomSettings.z);
        AddConnections();
    }

    private void InitializeGrid()
    {
        _GridPositions.Clear();
        for (int x = _MapBorders[0]; x < _MapBorders[2]; x++)
        {
            for (int y = _MapBorders[1]; y < _MapBorders[3]; y++)
            {
                _GridPositions.Add(new Vector3Int(x, y, 0));
            }
        }
    }

    //private void GenerateEmptyFloor()
    //{
    //    GenerateWallsAndFloors(new Vector3Int(_MapBorders[0], _MapBorders[1], 0), _MapBorders[2], _MapBorders[3], true);
    //}

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
                    if (!_FloorTileMap.HasTile(here))
                    {
                        _WallTileMap.SetTile(here, _WallTile);
                        newRoom.roomWalls.Add(here);                /*new TerrainTile(here, TileTypes.Wall)*/
                    }
                    else
                    {
                        newRoom = UpdateRoomOverlaps(newRoom, here);
                    }
                }
                else if (x > pos.x && x < columns && y > pos.y && y < rows)
                {
                    _FloorTileMap.SetTile(here, _FloorTile);
                    if (_WallTileMap.HasTile(here))
                    {
                        newRoom = UpdateRoomOverlaps(newRoom, here);
                        _WallTileMap.SetTile(here, null);
                    }
                    newRoom.roomFloors.Add(here);                   /*new TerrainTile(here, TileTypes.Floor)*/
                }
            }
        }
        _Rooms.Add(newRoom);
        if (newRoom.connected)
        {
            _ConnectedRooms.Add(newRoom);
        }
        else
        {
            _NotConnected.Add(newRoom);
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
            Vector3Int roomPos = _GridPositions[Random.Range(0, _GridPositions.Count)];
            GenerateWallsAndFloors(roomPos, roomColumns + roomPos.x, roomRows + roomPos.y, false);
        }
    }

    private Room UpdateRoomOverlaps(Room newRoom, Vector3Int currentTile)
    {
        foreach (Room room in _Rooms)
        {
            if (room.roomWalls.Contains(currentTile))
            {
                room.roomWalls.Remove(currentTile);
                room.roomFloors.Add(currentTile);
                if (room.overlappingRoomGroup == 0)
                {
                    _OverlappingGroupsCount++;
                    room.overlappingRoomGroup = _OverlappingGroupsCount;
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
        foreach (Room room in _Rooms)
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
                _NotConnected.Remove(room);
                _ConnectedRooms.Add(room);
            }
        }
    }

    private Vector3Int GetHallwayEndPoint()
    {
        Vector3Int endpoint;
        Room connectedRoom = _ConnectedRooms[Random.Range(0, _ConnectedRooms.Count)];
        endpoint = connectedRoom.roomFloors[Random.Range(0, connectedRoom.roomFloors.Count)];
        return endpoint;
    }

    private void CreateHallway(Vector3Int start, Vector3Int end)
    {
        int distance = ManhattanDistance(start, end);
        Vector3Int currentPoint = start;
        ReplaceWallWithFloor(currentPoint);
        AddDoorToRoom(currentPoint);
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

    private void ReplaceWallWithFloor(Vector3Int tilePosition)//replace wall with door
    {
        _WallTileMap.SetTile(tilePosition, null);
        _FloorTileMap.SetTile(tilePosition, _FloorTile);
    }

    private void AddDoorToRoom(Vector3Int tilePosition)
    {
        GameObject.Instantiate(_Door, tilePosition, Quaternion.identity);
    }

    private void FillEmptyWithWall(Vector3Int tilePosition)
    {
        if (!_FloorTileMap.HasTile(tilePosition))
        {
            _WallTileMap.SetTile(tilePosition, _WallTile);
        }
    }
}
