using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Being : MonoBehaviour
{
    protected string _Name;
    protected int _Health;
    protected int _Strength;
    protected int _ArmorClass;
    protected int _Experience;
    protected int _Gold;

    public void Move()
    {

    }

    public void Attack()
    {

    }

    public void Die()
    {

    }

    protected bool Occupied(int X, int Y, Tilemap _WallTile, Transform go)
    {
        if (_WallTile.GetTile(new Vector3Int((int)go.position.x + X, (int)go.position.y + Y, 0)))
        {
            //Debug.Log("Tile:" + new Vector3Int((int)transform.position.x + X, (int)transform.position.y + Y, 0));

            return true;
        }
        else
        {
            return false;
        }
    }
}
