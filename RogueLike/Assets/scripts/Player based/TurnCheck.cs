using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheck : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject _Player;


    private Vector2 _LastKnownPos;
    private Vector2 _CurrentPos;
    // Start is called before the first frame update
    void Start()
    {
        if (_Player == null) { FindPlayer(); }
        _CurrentPos = _Player.transform.position;
        _LastKnownPos = _CurrentPos;
    }

    // Update is called once per frame
    void Update()
    {
        _CurrentPos = _Player.transform.position;

        if (_LastKnownPos != _CurrentPos)
        {
            _LastKnownPos = _CurrentPos;
        }

    }
    private void FindPlayer()
    {
        _Player = GameObject.FindWithTag("Player");

        if (_Player != null)
        {
            Debug.Log("Found a \"Player\" object. Found Object: " + _Player);
        }
        else
        {
            Debug.Log("Couldn't find an object with the name or tag \"Player\"");
        }
    }
}
