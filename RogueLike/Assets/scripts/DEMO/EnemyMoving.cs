using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoving : MonoBehaviour
{
    public float _MoveDelay { private get; set; } = .2f;
    public DEMO _Demo { private get; set; }
    public Tilemap _WallTile { private get; set; }
    public bool _permitMove { private get; set; } = false;


    private float _HorizontalMoveDistance = 1f;
    private float _VerticalMoveDistance = 1f;
    private bool _StartedCoroutine = false;
    private Vector3 _newPos;
    private void Start()
    {
        _newPos = gameObject.transform.position;
    }
    private void Update()
    {
        if (_permitMove)
        {
            int X = Random.Range(-1, 2);
            int Y = Random.Range(-1, 2);
            TryMove(X, Y);
        }
    }

    void TryMove(int X, int Y)
    {
        if (!_Demo.Occupied(X, Y, _WallTile, this.transform))
        {
            _newPos += new Vector3(X, Y);
            gameObject.transform.position = _newPos + new Vector3(0, 0, -1);
            //Debug.Log(X + " " + Y);
        }
    }
}
