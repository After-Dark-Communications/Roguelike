using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Level_Generation
{
    class Room
    {
        public List<TerrainTile> roomWalls;
        public List<TerrainTile> roomFloors;
        public int overlappingRoomGroup;

        private Vector3Int bottomLeft;
        private int columns;
        private int rows;

        //private int roomID;
        //private bool reachable;

        public Room(Vector3Int origin, int columns, int rows)
        {
            roomWalls = new List<TerrainTile>();
            roomFloors = new List<TerrainTile>();
            overlappingRoomGroup = 0;
            this.bottomLeft = origin;
            this.columns = columns;
            this.rows = rows;
        }
    }
}
