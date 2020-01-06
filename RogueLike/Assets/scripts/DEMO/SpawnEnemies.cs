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
            Emov._permitMove = _TurnCheck._EnemyTurn;
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
}
