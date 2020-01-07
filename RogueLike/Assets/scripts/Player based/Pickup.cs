using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    public Item _item;
    public Transform _Player;

    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
