using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[SelectionBase]
public class PlayerMovement : Being
{
    [SerializeField] private float _MoveDelay = .2f;
    [SerializeField] private float _HorizontalMoveDistance = 1f;
    [SerializeField] private float _VerticalMoveDistance = 1f;
    private bool _permitMove = false;
    private bool _StartedCoroutine = false;
    private Vector3 _newPos;
    [SerializeField] private Tilemap _WallTile;
    private Interact interact;
    private void Start()
    {
        _newPos = gameObject.transform.position;
        interact = new Interact();
    }
    void Update()
    {
        //Debug.DrawLine(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0), new Vector3Int((int)transform.position.x + (int)Input.GetAxisRaw("Horizontal"), (int)transform.position.y + (int)Input.GetAxisRaw("Vertical"), 0), Color.white);
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (_permitMove)
            {
                TryMove();
            }
            if (!_StartedCoroutine)
            {
                StartCoroutine("Countdown");
            }
        }
        //}

    }

    void TryMove() //attempt to move in a given direction, checks if there is a wall or not
    {
        float X_Speed = Input.GetAxisRaw("Horizontal") * _HorizontalMoveDistance;
        float Y_Speed = Input.GetAxisRaw("Vertical") * _VerticalMoveDistance;

        if (!Occupied((int)X_Speed, (int)Y_Speed, _WallTile, this.transform))
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].transform.position == (_newPos + new Vector3(X_Speed, Y_Speed)))
                {
                    interact.TryOpenDoor(gameObject.GetComponent<Player>()._inventory, doors[i]);
                    return;
                }
            }

            _newPos += new Vector3(X_Speed, Y_Speed);
            gameObject.transform.position = _newPos + new Vector3(0, 0, -1);
        }
    }

    protected IEnumerator Countdown()
    {
        _StartedCoroutine = true;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            _permitMove = false;
            normalizedTime += Time.deltaTime / _MoveDelay;
            yield return null;
        }
        _permitMove = true;
        _StartedCoroutine = false;
    }


}