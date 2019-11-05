using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Level_Generation
{
    enum TileTypes { Wall, Floor, Door, Stairs }

    class TerrainTile
    {
        public Vector3Int position { get; }
        private TileTypes tileType;

        public TerrainTile(Vector3Int pos, TileTypes type)
        {
            this.position = pos;
            this.tileType = type;
        }
    }
}
