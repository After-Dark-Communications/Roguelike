using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    [SerializeField] private KeyCode _PickUp = KeyCode.G;
    [SerializeField] public Transform _Player;

    void Start()
    {
        _Player = GameObject.Find("Player").transform;
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
                    PickUp();
                }
            }
        }
    }

    private void PickUp()
    {
        // TODO: Add to inventory
        Destroy(gameObject);
    }
}
