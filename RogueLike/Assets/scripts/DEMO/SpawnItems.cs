using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private Tilemap _GroundTile;

    [Header("Item settings")]
    [SerializeField] public List<SpawnItem> _SpawnItem = new List<SpawnItem>();
    //[SerializeField] private ItemGear itemGear;
    //[SerializeField] private ItemConsumable itemConsumable; // TODO: Also do the consumables

    [HideInInspector] public List<GameObject> _SpawnedGo = new List<GameObject>();
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
                GameObject Sitem = new GameObject();
                Sitem.name = _SpawnItem[i].item.name + i;
                SpriteRenderer SpriteR = Sitem.AddComponent<SpriteRenderer>();
                //sprite setup
                SpriteR.sprite = _SpawnItem[i].item.spriteImage;
                SpriteR.color = _SpawnItem[i].item.spriteColor;
                SpriteR.sortingOrder = _SpawnItem[i].item.sortingOrder;
                //object setup
                Sitem.transform.SetParent(this.gameObject.transform);
                Sitem.tag = _SpawnItem[i].item.itemTag;
                Sitem.AddComponent<BoxCollider2D>().isTrigger = true;
                Pickup pick = Sitem.AddComponent<Pickup>();
                pick._item = _SpawnItem[i].item;
                _SpawnItem[i].pos = Sitem.transform;
                _SpawnedGo.Add(Sitem);
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

        foreach (GameObject Go in _SpawnedGo)
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
    [HideInInspector] public Transform pos;
}