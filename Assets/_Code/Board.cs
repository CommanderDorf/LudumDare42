using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;
    private Board _instance;
    
    [SerializeField] private int _boardSize = 10;
    [SerializeField] private List<GameObject> _tiles;
    private GameObject[,] _tileMap;
    private readonly int[] _rotations = new[] {0, 90, 180, 360};

    private List<GameObject[]> _rows;

    private GameObject _boardObj;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Instance = _instance;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _boardObj = new GameObject("Board");
    }

    void Start()
    {
        _tileMap = new GameObject[_boardSize,_boardSize];

        BoardSetUp();
    }

    private void BoardSetUp()
    {
        for (int x = 0; x < _boardSize; x++)
        {
            for (int y = 0; y < _boardSize; y++)
            {
                GameObject go = _tileMap[x, y] = Instantiate(_tiles[Random.Range(0, _tiles.Count)],
                    new Vector3(x, y),
                    Quaternion.Euler(0, 0, _rotations[Random.Range(0, _rotations.Length)]));
                go.name = x + "_" + y;
                go.transform.parent = _boardObj.transform;
            }
        }
    }

    /*
    public GameObject PushTile(GameObject tile, Vector2Int pos)
    {
        // Right side of the board
        if (pos.x == _boardSize)
        {
            int y = pos.y;

            GameObject currentTile;
            GameObject extraTile = _tileMap[0, y];
            extraTile.SetActive(false);
            
            for (int x = 0; x < _boardSize - 1; x++)
            {
                currentTile = _tileMap[x, y];
                
                currentTile.transform.position += Vector3.left;
                _tileMap[x, y] = _tileMap[x + 1, y];
            }

            currentTile = _tileMap[_boardSize - 1, y];
            currentTile.transform.position += Vector3.left;

            tile.transform.position = new Vector3(_boardSize - 1, y);
            _tileMap[_boardSize - 1, y] = tile;

            extraTile.transform.position = new Vector3(pos.x, pos.y);
            extraTile.SetActive(true);
            return extraTile;
        }
        // Left side of the board
        else if (pos.x == -1)
        {
            int y = pos.y;

            GameObject currentTile;
            GameObject extraTile = _tileMap[_boardSize - 1, y];
            extraTile.SetActive(false);
            
            for (int x = _boardSize - 1; x > 0; x--)
            {
                currentTile = _tileMap[x, y];
                
                currentTile.transform.position += Vector3.right;
                _tileMap[x, y] = _tileMap[x -1, y];
            }

            currentTile = _tileMap[0, y];
            currentTile.transform.position += Vector3.right;

            tile.transform.position = new Vector3(0, y);
            _tileMap[0, y] = tile;

            extraTile.transform.position = new Vector3(pos.x, pos.y);
            extraTile.SetActive(true);
            return extraTile;
        }
        
        // Top of the board
        if (pos.y == _boardSize)
        {
            int x = pos.x;

            GameObject currentTile;
            GameObject extraTile = _tileMap[x, 0];
            extraTile.SetActive(false);
            
            for (int y = 0; y < _boardSize - 1; y++)
            {
                currentTile = _tileMap[x, y];
                
                currentTile.transform.position += Vector3.down;
                _tileMap[x, y] = _tileMap[x, y + 1];
            }

            currentTile = _tileMap[x, _boardSize - 1];
            currentTile.transform.position += Vector3.down;

            tile.transform.position = new Vector3(x, _boardSize - 1);
            _tileMap[x, _boardSize - 1] = tile;

            extraTile.transform.position = new Vector3(pos.x, pos.y);
            extraTile.SetActive(true);
            return extraTile;
        }
        // Bottom of the board
        else if (pos.y == -_boardSize)
        {

        }

        return null;
    }
    */
}