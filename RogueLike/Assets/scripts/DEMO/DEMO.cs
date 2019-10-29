using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DEMO : MonoBehaviour
{
    [SerializeField] private Sprite _SpriteIcon;

    private SpriteRenderer _PO;
    private void Awake()
    {
        _PO = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _PO.sprite = _SpriteIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
