using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    [SerializeField] private KeyCode _PickUp = KeyCode.G;
    public Transform _Player;
    private Interact _Interact;
    void Start()
    {
        _Player = GameObject.Find("Player").transform;
        _Interact = new Interact(5);
    }

    private void OnEnable()
    {
        _Interact = new Interact(5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_PickUp))
        {
            if (_Player != null)
            {
                if (gameObject.transform.position.x == _Player.transform.position.x &&
                    gameObject.transform.position.y == _Player.transform.position.y)
                {
                    //Debug.Log("Item " + gameObject.name + " picked up.");
                    _Interact.PickupItem(PickUp());
                }
            }
        }
    }

    private GameObject PickUp()
    {
        return gameObject;
    }
}
