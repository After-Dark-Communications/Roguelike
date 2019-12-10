using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private Tilemap _GroundTile;

    [Header("Item settings")]
    [SerializeField] private List<SpawnItem> _SpawnItem = new List<SpawnItem>();
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
        for (int i = 0; i < _SpawnItem.Count; i++)
        {
            for (int p = 0; p < _SpawnItem[i]._ItemsToSpawn; p++)
            {
                GameObject Item = new GameObject();
                Item.name = _SpawnItem[i].item.name + i;
                SpriteRenderer SpriteR = Item.AddComponent<SpriteRenderer>();
                //sprite setup
                SpriteR.sprite = _SpawnItem[i].item.spriteImage;
                SpriteR.color = _SpawnItem[i].item.spriteColor;
                SpriteR.sortingOrder = _SpawnItem[i].item.sortingOrder;
                //object setup
                Item.transform.SetParent(this.gameObject.transform);
                Item.tag = _SpawnItem[i].item.itemTag;
                Item.AddComponent<BoxCollider2D>().isTrigger = true;
                Item.AddComponent<Pickup>();
                _SpawnedItems.Add(Item);
            }
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

[System.Serializable]
public class SpawnItem
{
    public int _ItemsToSpawn;
    public Item item;
}