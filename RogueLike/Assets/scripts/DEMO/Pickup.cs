using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    [SerializeField] private KeyCode _PickUp = KeyCode.G;
    [SerializeField] private Transform _Player;
    private Tilemap _TM;

    private void Awake()
    {
        _TM = gameObject.GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_PickUp))
        {
            TileBase CurPlayer = _TM.GetTile(new Vector3Int((int)_Player.position.x - 1, (int)_Player.position.y - 1, 0));
            Debug.Log(CurPlayer);
            if (CurPlayer != null)
            {
                Vector3Int cell = _TM.WorldToCell(_Player.position);
                _TM.SetTile(new Vector3Int((int)_TM.CellToWorld(cell).x, (int)_TM.CellToWorld(cell).y, (int)_TM.CellToWorld(cell).z), null);
                Debug.Log("Sucked it right up the anus.");
            }
        }
    }
}
