using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Being : MonoBehaviour
{
    [SerializeField] protected string _Name;
    [SerializeField] protected int _Health;
    [SerializeField] protected int _Strength;
    [SerializeField] protected int _ArmorClass;
    [SerializeField] protected int _Experience;
    [SerializeField] protected int _Gold;
    [SerializeField] protected int _MaxHealth;

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

    protected bool Occupied2(int X, int Y, Tilemap _WallTile, Transform go)
    {
        if (_WallTile.GetTile(new Vector3Int(X, Y, 0)))
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
