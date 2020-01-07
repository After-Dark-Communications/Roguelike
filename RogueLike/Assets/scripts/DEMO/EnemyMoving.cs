using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

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
            int X = UnityEngine.Random.Range(-1, 2);
            int Y = UnityEngine.Random.Range(-1, 2);
            TryMove(X, Y);
        }
    }

    public void MoveRandom()
    {
        int X = UnityEngine.Random.Range(-1, 2);
        int Y = UnityEngine.Random.Range(-1, 2);
        TryMove(X, Y);
    }

    public bool TryMove(int X, int Y)
    {
        if (!Occupied2(X, Y, _WallTile, this.transform)) //check for wall
        {
            for (int i = 0; i < _Doors.Length; i++)
            {
                Vector2 Doorpos = _Doors[i].transform.localPosition;
                if (Doorpos == ((Vector2)_newPos + new Vector2(X,Y)) && _Doors[i].activeSelf)
                {
                    return false;
                }
            }
            if(CheckMonster(X, Y))
            {
                return false;
            }
            if (!CheckPlayer(X, Y)) //check for player
            {
                gameObject.transform.position = new Vector3(X, Y, 0);
                //Debug.Log(X + " " + Y);
                _newPos = gameObject.transform.position;
                return true;
            }
            else
            {
                Debug.Log("Attack Player at: " + (gameObject.transform.position.x + X) + " ," + (gameObject.transform.position.y + Y));
            }
        }
        return false;
    }


    public List<GameObject> _SpawnedEnemies;

    public bool CheckMonster(int X, int Y)
    {
        foreach (GameObject Mon in _SpawnedEnemies)
        {
            if (Mon.transform.position.x == X && Mon.transform.position.y == Y)
            {
                return true;
            }
        }
        return false;
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

    public void FindNextTile(Vector3Int startTile, Vector3Int endTile)
    {
        Vector3Int nextTile = new Vector3Int();

        //x
        if (startTile.x > endTile.x)
        {
            nextTile.x = nextTile.x - 1;
        }
        if (startTile.x == endTile.x)
        {
            nextTile.x = nextTile.x;
        }
        if (startTile.x < endTile.x)
        {
            nextTile.x = nextTile.x + 1;
        }

        //y
        if (startTile.y > endTile.y)
        {
            nextTile.y = nextTile.y - 1;
        }
        if (startTile.y == endTile.y)
        {
            nextTile.y = nextTile.y;
        }
        if (startTile.y < endTile.y)
        {
            nextTile.y = nextTile.y + 1;
        }

        _newPos += new Vector3Int(nextTile.x, nextTile.y, 0);
        //int tryCounter = 0;

        TryMove(Convert.ToInt32(_newPos.x), Convert.ToInt32(_newPos.y));

        //while (TryMove(Convert.ToInt32(_newPos.x), Convert.ToInt32(_newPos.y)) == false)
        //{
        //    nextTile = new Vector3Int();
        //    //x
        //    if (startTile.x > endTile.x)
        //    {
        //        nextTile.x = nextTile.x - 1;
        //    }
        //    if (startTile.x == endTile.x)
        //    {
        //        nextTile.x = nextTile.x;
        //    }
        //    if (startTile.x < endTile.x)
        //    {
        //        nextTile.x = nextTile.x + 1;
        //    }

        //    //y
        //    if (startTile.y > endTile.y)
        //    {
        //        nextTile.y = nextTile.y - 1;
        //    }
        //    if (startTile.y == endTile.y)
        //    {
        //        nextTile.y = nextTile.y;
        //    }
        //    if (startTile.y < endTile.y)
        //    {
        //        nextTile.y = nextTile.y + 1;
        //    }

        //    _newPos += new Vector3Int(nextTile.x, nextTile.y, 0);

        //    //tryCounter++;

        //    //if (tryCounter > 1)
        //    //{
        //    //    break;
        //    //}
        //}
        _newPos = gameObject.transform.position;
    }
}
