using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelCreator))]
public class LevelProgression : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Transform _Player;
    [SerializeField] private int _DeeperScene = 1;
    [SerializeField] private int _HigerhScene = 0;
    [SerializeField] private Tilemap _GroundTile;
    [SerializeField] private Tilemap _EntryWayTile;
    [Header("Visuals")]
    [Tooltip("The tile to go to the next level")]
    [SerializeField] private Tile _DeeperTile;
    [Tooltip("The tile to go to the previous level")]
    [SerializeField] private Tile _SurfaceTile;
    [SerializeField] private Color _TileColor = Color.grey;

    private LevelCreator _Level;
    private Vector3Int _DeeperPos = new Vector3Int();
    private Vector3Int _SurfacePos = new Vector3Int();

    private void Awake()
    {
        _DeeperScene = SceneManager.GetActiveScene().buildIndex + 1;
        _HigerhScene = SceneManager.GetActiveScene().buildIndex - 1;
        if (_HigerhScene <= 1)
        {
            _HigerhScene = SceneManager.GetActiveScene().buildIndex;
            _SurfaceTile = null;
        }
        _Level = gameObject.GetComponent<LevelCreator>();
    }
    private void Start()
    {
        //place entryway
        PlaceEntry();
        if (_DeeperTile != null) { Debug.Log("Deeper " + _DeeperPos); }
        if (_SurfaceTile != null) { Debug.Log("Surface " + _SurfacePos); }
    }

    private void Update()
    {
        if (_Player.position == _DeeperPos && _DeeperTile != null)
        {
            SceneManager.LoadScene(_DeeperScene);
            Debug.Log("Go to Level(" + _DeeperScene + ")");
        }

        if (_Player.position == _SurfacePos && _SurfaceTile != null)
        {
            SceneManager.LoadScene(_HigerhScene);
            Debug.Log("Go to Level(" + _HigerhScene + ")");
        }
    }

    private void PlaceEntry()
    {
        List<Vector3Int> AllTiles = new List<Vector3Int>();
        for (int x = 0; x < _Level._Rows; x++)
        {
            for (int y = 0; y < _Level._Columns; y++)
            {
                if (x != 0 && y != 0)
                {
                    if (x != -1 && y != 0)
                    {
                        Vector3Int currentpos = new Vector3Int(x, y, 0);
                        if (_GroundTile.HasTile(currentpos))
                        {
                            AllTiles.Add(currentpos);
                        }
                    }
                }
            }
        }
        int tilepos = Random.Range(0, AllTiles.Count);
        _EntryWayTile.color = _TileColor;
        //place deeper tile
        _DeeperPos = AllTiles[tilepos] + new Vector3Int(0, 0, -1);
        setTileInScene(_DeeperPos, _DeeperTile);
        //plave surface tile
        _SurfacePos = new Vector3Int(-1, 0, -1);
        setTileInScene(_SurfacePos, _SurfaceTile);
    }

    private void setTileInScene(Vector3Int pos, Tile placetile)
    {
        if (placetile != null)
        {
            _EntryWayTile.SetTile(pos, null);
            _EntryWayTile.SetTile(pos, placetile);
        }
    }

}

//monochrome 195 for deeper
//monochrome 194 for surface