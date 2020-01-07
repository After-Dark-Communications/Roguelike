using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemies : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap _WallTile;
    [SerializeField] private Tilemap _GroundTile;
    [SerializeField] private TurnCheck _TurnCheck;
    [Header("Enemy settings")]
    [SerializeField] private string _EnemyTag = "Enemy";
    [SerializeField] private string _PlayerTag = "Player";
    [SerializeField] private int _SortingOrder;
    [SerializeField] private int _EnemiesToSpawn;
    [Header("sprite settings")]
    [SerializeField] private Color _EnemyColor = Color.green;
    [SerializeField] private List<Sprite> _EnemySprites = new List<Sprite>();

    private List<GameObject> _SpawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        SpawnEnemy();
    }
    void Start()
    {
        PlaceEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject Go in _SpawnedEnemies)
        {
            EnemyMoving Emov = Go.GetComponent<EnemyMoving>();
            if(_TurnCheck._EnemyTurn)
            {
                AI(Emov, Vector3Int.RoundToInt(Go.transform.position));
            }
        }
    }

    private void SpawnEnemy()
    {

        for (int i = 0; i < _EnemiesToSpawn; i++)
        {
            GameObject Enemy = new GameObject();
            Enemy.name = "Enemy" + i;
            SpriteRenderer SpriteR = Enemy.AddComponent<SpriteRenderer>();
            EnemyMoving Emov = Enemy.AddComponent<EnemyMoving>();
            // Stats
            Enemy.GetComponent<EnemyMoving>()._MaxHealth = Random.Range(1, 11);
            Enemy.GetComponent<EnemyMoving>()._Health = Enemy.GetComponent<EnemyMoving>()._MaxHealth;
            Enemy.GetComponent<EnemyMoving>()._Strength = Random.Range(1, 5);
            Enemy.GetComponent<EnemyMoving>()._Experience = Random.Range(1, 20);
            //sprite setup
            SpriteR.sprite = _EnemySprites[Random.Range(0, _EnemySprites.Count)];
            SpriteR.color = _EnemyColor;
            SpriteR.sortingOrder = _SortingOrder;
            //movement setup
            Emov._WallTile = _WallTile;
            Emov._PlayerTag = _PlayerTag;
            //object setup
            Enemy.transform.SetParent(this.gameObject.transform);
            Enemy.tag = _EnemyTag;


            _SpawnedEnemies.Add(Enemy);
        }
    }

    private void PlaceEnemy()
    {
        List<Vector3> Tiles = new List<Vector3>();

        for (int x = -50; x < 50; x++)
        {
            for (int y = -50; y < 50; y++)
            {
                if (_GroundTile.GetTile(new Vector3Int(x, y, 0)) && (x != 0 && y != 0))
                {
                    Tiles.Add(new Vector3(x, y));
                }
            }
        }

        foreach (GameObject Go in _SpawnedEnemies)
        {
            int x = Random.Range(0, Tiles.Count);
            Go.transform.position = Tiles[x];
            Tiles.Remove(Tiles[x]);
        }

    }

    public void AI(EnemyMoving eMov, Vector3Int CPos)
    {
        Vector3Int playerLocation = Vector3Int.RoundToInt(GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector3Int CurrentLocation = CPos;
        Vector3Int LastKnownPlayerLocation = new Vector3Int(10000, 10000, 0);
        //int TurnsSinceLastPlayerSighting = 100;
        eMov._SpawnedEnemies = _SpawnedEnemies;

        if (IsPlayerInSight(playerLocation, CurrentLocation))
        {
            if (IsPlayerInAttackRange(playerLocation, CurrentLocation))
            {
                //Attack();
            }
            else
            {
                eMov.FindNextTile(CurrentLocation, playerLocation);
            }
            LastKnownPlayerLocation = playerLocation;
            //TurnsSinceLastPlayerSighting = 0;
        }
        else
        {
            //TurnsSinceLastPlayerSighting++;
            //if (TurnsSinceLastPlayerSighting < 8)
            //{
            //    if (LastKnownPlayerLocation == CurrentLocation)
            //    {
            //        eMov.MoveRandom();
            //    }
            //    else
            //    {
            //        eMov.FindNextTile(CurrentLocation, playerLocation);
            //    }
            //}
        }
    }

    private bool IsPlayerInAttackRange(Vector3Int playerLocation, Vector3Int CurrentLocation)
    {
        if (playerLocation.x == CurrentLocation.x + 1 && playerLocation.y == CurrentLocation.y + 1)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x + 1 && playerLocation.y == CurrentLocation.y)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x + 1 && playerLocation.y == CurrentLocation.y - 1)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x && playerLocation.y == CurrentLocation.y + 1)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x && playerLocation.y == CurrentLocation.y)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x && playerLocation.y == CurrentLocation.y - 1)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x - 1 && playerLocation.y == CurrentLocation.y + 1)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x - 1 && playerLocation.y == CurrentLocation.y)
        {
            return true;
        }
        if (playerLocation.x == CurrentLocation.x - 1 && playerLocation.y == CurrentLocation.y - 1)
        {
            return true;
        }
        return false;
    }

    private bool IsPlayerInSight(Vector3Int playerLocation, Vector3Int CurrentLocation)
    {
        int sightRange = 6;
        // Dus gewoon als de speler binnen X vakjes van het monster is...
        if (playerLocation.x < CurrentLocation.x + sightRange && playerLocation.y < CurrentLocation.y + sightRange && playerLocation.x > CurrentLocation.x - sightRange && playerLocation.y > CurrentLocation.y - sightRange)
        {
            return true;
        }

        return false;
    }
}
