using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    [SerializeField] private GameObject _newTile;

    private Vector3 _mouseWorldPos;
    private Camera _cam;
    private readonly Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
    // Use this for initialization
    void Start()
    {
        _cam = Camera.main;

        _newTile = Instantiate(_newTile);
    }

    // Update is called once per frame
    void Update()
    {
        _mouseWorldPos = _cam.ScreenToWorldPoint(Input.mousePosition) + _offset;
        _mouseWorldPos.x = Mathf.FloorToInt(_mouseWorldPos.x);
        _mouseWorldPos.y = Mathf.FloorToInt(_mouseWorldPos.y);
        _mouseWorldPos.z = 0;

        _cursor.transform.position = _mouseWorldPos;
        
        
        if(_newTile != null)
            _newTile.transform.position = _mouseWorldPos;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int pos = new Vector2Int((int)_mouseWorldPos.x, (int)_mouseWorldPos.y);
            //_newTile = Board.Instance.PushTile(_newTile, pos);
        }

    }
}