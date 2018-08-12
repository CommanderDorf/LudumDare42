using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private GameObject _newTile;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _errorSound;
    [SerializeField] private GameObject _curseWords;

    public bool PlayerHasControl = true;
    
    private Vector3 _mouseWorldPos;
    private Camera _cam;
    private readonly Vector3 _offset = new Vector3(0.5f, 0.5f, 0);
    
    // Use this for initialization
    void Start()
    {
        _cam = Camera.main;

        _newTile = Instantiate(_newTile);

        foreach (Transform obj in _newTile.GetComponentsInChildren<Transform>())
        {
            obj.gameObject.layer = LayerMask.NameToLayer("Cursor");
            SpriteRenderer r = obj.GetComponent<SpriteRenderer>();
            if (r != null)
            {
                r.sortingLayerName = "Hidden";
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerHasControl) return;
        
        
        _mouseWorldPos = _cam.ScreenToWorldPoint(Input.mousePosition) + _offset;
        _mouseWorldPos.x = Mathf.FloorToInt(_mouseWorldPos.x);
        _mouseWorldPos.y = Mathf.FloorToInt(_mouseWorldPos.y);
        _mouseWorldPos.z = 0;
        
        if(_newTile != null)
            _newTile.transform.position = _mouseWorldPos;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int pos = new Vector2Int((int)_mouseWorldPos.x, (int)_mouseWorldPos.y);
            
            
            // We don't allow pushing rows where the player is standing
            if (Mathf.FloorToInt(_player.transform.position.x + _offset.x) == (int) _mouseWorldPos.x)
            {
                // Play Error Soundtrack
                _errorSound.Play();
                
                // Make Player angry for visual hint
                GameObject curseWordGo = Instantiate(_curseWords, _player.transform);
                Destroy(curseWordGo, 0.5f);
                return;
            }

            if (Mathf.FloorToInt(_player.transform.position.y + _offset.y) == (int) _mouseWorldPos.y)
            {
                // Play Error Soundtrack
                _errorSound.Play();
                
                // Make Player angry for visual hint
                GameObject curseWordGo = Instantiate(_curseWords, _player.transform);
                Destroy(curseWordGo, 0.5f);
                return;
            }
            
            GameObject go = Board.Instance.PushTile(_newTile, pos);;

            if (go != null)
            {
                if (_newTile != null)
                {
                    foreach (Transform obj in _newTile.GetComponentsInChildren<Transform>())
                    {
                        obj.gameObject.layer = LayerMask.NameToLayer("Level");
                        SpriteRenderer r = obj.GetComponent<SpriteRenderer>();
                        if (r != null)
                        {
                            r.sortingLayerName = "Default";
                        }
                    }
                }
                _newTile = go;
                
                foreach (Transform obj in _newTile.GetComponentsInChildren<Transform>())
                {
                    obj.gameObject.layer = LayerMask.NameToLayer("Cursor");
                    SpriteRenderer r = obj.GetComponent<SpriteRenderer>();
                    if (r != null)
                    {
                        r.sortingLayerName = "Hidden";
                    }
                }
            }

        }

        if (Input.GetMouseButtonDown(1) && _newTile != null)
        {
            _newTile.transform.Rotate(0, 0, -90);
        }

    }
}