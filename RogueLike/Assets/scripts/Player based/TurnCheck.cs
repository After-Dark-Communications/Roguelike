using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheck : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject _Player;
    [SerializeField] private Camera _FieldCamera;

    private Vector2 _LastKnownPos;
    [SerializeField] private Vector2 _CurrentPos;
    [SerializeField] private Vector2 _ScreenSize;

    internal bool _EnemyTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_Player == null) { FindPlayer(); }
        _CurrentPos = _Player.transform.position;
        _LastKnownPos = _CurrentPos;
        if (_FieldCamera != null)
        {
            _ScreenSize.x = Screen.width;
            _ScreenSize.y = Screen.height;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _CurrentPos = _Player.transform.position;

        if (_LastKnownPos != _CurrentPos)
        {
            _LastKnownPos = _CurrentPos;
            _EnemyTurn = true;
        }
        else
        {
            _EnemyTurn = false;
        }

    }
    private void FindPlayer()
    {
        _Player = GameObject.FindWithTag("Player");

        if (_Player == null)
        {
            Debug.Log("Couldn't find an object with the name or tag \"Player\"");
        }
        else
        {
            //Debug.Log("Found a \"Player\" object. Found Object: " + _Player);
        }
    }
}
