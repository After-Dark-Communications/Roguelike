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
        public List<Vector3Int> roomWalls;
        public List<Vector3Int> roomFloors;
        public List<Vector3Int> openings;
        public int overlappingRoomGroup;
        public bool connected;

        private Vector3Int bottomLeft;
        private int columns;
        private int rows;
        private Vector3Int[] corners;

        //private int roomID;
        //private bool reachable;

        public Room(Vector3Int origin, int columns, int rows, bool startRoom)
        {
            roomWalls = new List<Vector3Int>();
            roomFloors = new List<Vector3Int>();
            openings = new List<Vector3Int>();
            overlappingRoomGroup = 0;
            this.bottomLeft = origin;
            this.columns = columns;
            this.rows = rows;
            this.connected = startRoom;
            corners = new Vector3Int[4] { 
                origin, 
                new Vector3Int(origin.x, origin.y + rows - 1, 0), 
                new Vector3Int(origin.x + columns - 1, origin.y, 0),
                new Vector3Int(origin.x + columns - 1, origin.y + rows - 1, 0) 
            };
        }

        //public List<Vector3Int> GetNonCornerWalls()
        //{
        //    List<Vector3Int> straightWalls = roomWalls;
        //    foreach (Vector3Int corner in corners)
        //    {
        //        straightWalls.Remove(corner);
        //    }
        //    return straightWalls;
        //}

        //public void SetOpening(Vector3Int wallHole)
        //{
        //    if (roomWalls.Contains(new Vector3Int(wallHole.x + 1, wallHole.y, 0)))  // is there a wall to the left of the hole
        //    {
        //        // if true we know there must be another wall on the left
        //        if (roomFloors.Contains(new Vector3Int(wallHole.x, wallHole.y + 1, 0)))    // is there a floor above the hole
        //        {
        //            // if true we know the hallway must be connected below the hole
        //            openings.Add(new Vector3Int(wallHole.x, wallHole.y - 1, 0));
        //        }
        //        else
        //        {
        //            // if false we know the hallway must be connected above the hole
        //            openings.Add(new Vector3Int(wallHole.x, wallHole.y + 1, 0));
        //        }
        //    }
        //    else
        //    {
        //        // if false we know the walls are above and below the hole
        //        if (roomFloors.Contains(new Vector3Int(wallHole.x + 1, wallHole.y, 0))) // is there a floor to the right
        //        {
        //            // if true we know the hallway must be connected to the left of the hole
        //            openings.Add(new Vector3Int(wallHole.x - 1, wallHole.y, 0));
        //        }
        //        else
        //        {
        //            // if false we know the hallway must be connected to the right of the hole
        //            openings.Add(new Vector3Int(wallHole.x + 1, wallHole.y, 0));
        //        }
        //    }
        //}
    }
}
