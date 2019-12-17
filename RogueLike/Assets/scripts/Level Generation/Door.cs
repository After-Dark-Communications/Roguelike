using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Color DoorColor = Color.white;
    public Sprite LockedSprite;
    public Sprite UnLockedSprite;
    public bool IsLocked = false;

    private void Awake()
    {
        SpriteRenderer Render = GetComponent<SpriteRenderer>();
        Render.color = DoorColor;
        if (IsLocked)
        {
            Render.sprite = LockedSprite;
        }
        else
        {
            Render.sprite = UnLockedSprite;
        }
    }
}
