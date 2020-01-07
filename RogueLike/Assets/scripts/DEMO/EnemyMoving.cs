using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMoving : Being
{
    public float _MoveDelay { private get; set; } = .2f;
    public Tilemap _WallTile { private get; set; }
    public bool _permitMove { private get; set; } = false;
    public string _PlayerTag { private get; set; }

    private Vector2 _PlayerPos;
    private Vector3 _newPos;
    private GameObject[] _Doors;
    private void Start()
    {
        _Doors = GameObject.FindGameObjectsWithTag("Door");
        _PlayerPos = GameObject.FindGameObjectWithTag(_PlayerTag).GetComponent<Transform>().position;
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

    public void MoveRandom()
    {
        int X = Random.Range(-1, 2);
        int Y = Random.Range(-1, 2);
        TryMove(X, Y);
    }

    public void TryMove(int X, int Y)
    {
        if (!Occupied(X, Y, _WallTile, this.transform)) //check for wall
        {
            for (int i = 0; i < _Doors.Length; i++)
            {
                Vector2 Doorpos = _Doors[i].transform.localPosition;
                if (Doorpos == ((Vector2)_newPos + new Vector2(X,Y)) && _Doors[i].activeSelf)
                {
                    return;
                }
            }
            if (!CheckPlayer(X, Y)) //check for player
            {
                _newPos += new Vector3(X, Y);
                gameObject.transform.position = _newPos + new Vector3(0, 0, -1);
                //Debug.Log(X + " " + Y);
            }
            else
            {
                Debug.Log("Attack Player at: " + (gameObject.transform.position.x + X) + " ," + (gameObject.transform.position.y + Y));
            }
        }
    }

    private bool CheckPlayer(int X, int Y)
    {
        _PlayerPos = GameObject.FindGameObjectWithTag(_PlayerTag).GetComponent<Transform>().position;
        Vector2 MoveDir = new Vector2(gameObject.transform.position.x + X, gameObject.transform.position.y + Y);
        if (MoveDir == _PlayerPos)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
