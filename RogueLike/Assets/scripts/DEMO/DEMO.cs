using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DEMO : MonoBehaviour
{
    [SerializeField] private Transform _Plane;
    // Start is called before the first frame update
    void Start()
    {
        _Plane.gameObject.SetActive(false);
    }

    internal bool Occupied(int X, int Y, Tilemap _WallTile, Transform go)
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
