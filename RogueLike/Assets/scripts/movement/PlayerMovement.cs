﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[SelectionBase]
public class PlayerMovement : Being
{
    [SerializeField] private float _MoveDelay = .2f;
    [SerializeField] private float _HorizontalMoveDistance = 1f;
    [SerializeField] private float _VerticalMoveDistance = 1f;
    [SerializeField] private string _EnemyTag = "Enemy";
    private bool _permitMove = false;
    private bool _StartedCoroutine = false;
    private Vector3 _newPos;
    [SerializeField] private Tilemap _WallTile;
    private void Start()
    {
        _newPos = gameObject.transform.position;
    }
    void Update()
    {
        //Debug.DrawLine(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0), new Vector3Int((int)transform.position.x + (int)Input.GetAxisRaw("Horizontal"), (int)transform.position.y + (int)Input.GetAxisRaw("Vertical"), 0), Color.white);
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
    }

    void TryMove()
    {
        float X_Speed = Input.GetAxisRaw("Horizontal") * _HorizontalMoveDistance;
        float Y_Speed = Input.GetAxisRaw("Vertical") * _VerticalMoveDistance;

        if (!Occupied((int)X_Speed, (int)Y_Speed, _WallTile, this.transform))
        {
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

//A B C D E F G H I J K L M N O P Q
//R S T U V W X Y Z a b c d e f g h 
//i j k l m n o p q r s t u v w x y 
//z 1 2 3 4 5 6 7 8 9 0 ! @ # $ % ^ 
//& * ( ) - = _ + ` ~ : ; { } [ ] | 
//\ < > , . ? / ☺ ♪ └ ╠ ← ╪ Ω ÷ ╢ ☻ 
//♫ ┴ ═ ∟ ┘ δ ≈ ╖ ♥ ☼ ┬ ╬ ↔ ┌ ∞ ° ╕ 
//♦ ► ├ ╧ ▲ █ φ ∙ ╣ ♣ ◄ ─ ╨ ▼ ▄ ε ╗ 
//║ ♠ ↕ ┼ ╤ # ▌ ∩ ╝ ¤ • ‼ ╞ § ╥ ▐ ≡ 
//░ † ◘ ¶ ╟ ▬ ╙ ▀ ± ▒ ‡ ○ ‰ ╚ ↨ ╘ µ 
//≥ ▓ ⌐ ◙ ≈ ╔ ↑ ╒ τ ≤ │ ¬ ♂ ╛ ╩ ↓ ╓ 
//Φ ⌠ ┤ º ♀ ┐ ╦ → ╫ Θ ⌡ ╡ I
