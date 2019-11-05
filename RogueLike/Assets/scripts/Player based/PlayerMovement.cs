using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[SelectionBase]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _MoveDelay = .2f;
    [SerializeField] private float _HorizontalMoveDistance = .25f;
    [SerializeField] private float _VerticalMoveDistance = .5f;

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
        Debug.DrawLine(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0), new Vector3Int((int)transform.position.x + (int)Input.GetAxisRaw("Horizontal"), (int)transform.position.y + (int)Input.GetAxisRaw("Vertical"), 0), Color.white);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (_permitMove)
            {
                //MoveCharacter();
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

        if (!Occupied((int)X_Speed, (int)Y_Speed))
        {
            _newPos += new Vector3(X_Speed, Y_Speed);
            gameObject.transform.position = _newPos + new Vector3(0,0,-1);
        }
        else
        {
            Debug.Log("You Buffoon, that's a wall!");
        }
    }

    bool Occupied(int X, int Y)
    {
        if (_WallTile.GetTile(new Vector3Int((int)gameObject.transform.position.x + X , (int)gameObject.transform.position.y + Y, 0)))
        {
            //Debug.Log("Tile:" + new Vector3Int((int)transform.position.x + X, (int)transform.position.y + Y, 0));
            
            return true;
        }
        else
        {
            return false;

        }
    }

    //private void MoveCharacter()
    //{
    //    float X_Speed = Input.GetAxisRaw("Horizontal") * _HorizontalMoveDistance;
    //    float Y_Speed = Input.GetAxisRaw("Vertical") * _VerticalMoveDistance;

    //    newPos += new Vector2(X_Speed, Y_Speed);
    //    //Debug.Log("newPos: " + newPos);
    //    gameObject.transform.position = newPos;

    //}



    private IEnumerator Countdown()
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

//☺ ♪ └ ╠ ← ╪ Ω ÷ ╢
//☻ ♫ ┴ ═ ∟ ┘ δ ≈ ╖
//♥ ☼ ┬ ╬ ↔ ┌ ∞ ° ╕
//♦ ► ├ ╧ ▲ █ φ ∙ ╣
//♣ ◄ ─ ╨ ▼ ▄ ε ╗ ║
//♠ ↕ ┼ ╤ # ▌ ∩ ╝ ¤
//• ‼ ╞ § ╥ ▐ ≡ ░ †
//◘ ¶ ╟ ▬ ╙ ▀ ± ▒ ‡
//○ ‰ ╚ ↨ ╘ µ ≥ ▓ ⌐
//◙ ≈ ╔ ↑ ╒ τ ≤ │ ¬
//♂ ╛ ╩ ↓ ╓ Φ ⌠ ┤ º
//♀ ┐ ╦ → ╫ Θ ⌡ ╡ I

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

