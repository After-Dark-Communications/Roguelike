using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private Tilemap _GroundTile;

    [Header("Item settings")]
    [SerializeField] private int _ItemsToSpawn;
    [SerializeField] private Item item;
    //[SerializeField] private ItemGear itemGear;
    //[SerializeField] private ItemConsumable itemConsumable; // TODO: Also do the consumables

    private List<GameObject> _SpawnedItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
        PlaceItem();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: maybe check here if item is being picked up??
    }

    private void SpawnItem()
    {
        for (int i = 0; i < _ItemsToSpawn; i++)
        {
            GameObject Item = new GameObject();
            Item.name = item.name + i;
            SpriteRenderer SpriteR = Item.AddComponent<SpriteRenderer>();
            //sprite setup
            SpriteR.sprite = item.spriteImage;
            SpriteR.color = item.spriteColor;
            SpriteR.sortingOrder = item.sortingOrder;
            //object setup
            Item.transform.SetParent(this.gameObject.transform);
            Item.tag = item.itemTag;
            Item.AddComponent<BoxCollider2D>().isTrigger = true;
            Item.AddComponent<Pickup>();
            _SpawnedItems.Add(Item);
        }
    }

    private void PlaceItem()
    {
        List<Vector3> Tiles = new List<Vector3>();

        for (int x = -50; x < 50; x++)
        {
            for (int y = -50; y < 50; y++)
            {
                if (_GroundTile.GetTile(new Vector3Int(x, y, 0)))
                {
                    Tiles.Add(new Vector3(x, y));
                }
            }
        }

        foreach (GameObject Go in _SpawnedItems)
        {
            Go.transform.position = Tiles[Random.Range(0, Tiles.Count)];
        }
    }
}
