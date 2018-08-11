using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    
    public GameObject TileGo;
    public Sprite TileSprite
    {
        get { return _sprite; }
    }

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        TileGo = gameObject;

        SetSprite();
    }

    private void SetSprite()
    {
        _renderer.sprite = _sprite;
    }
}