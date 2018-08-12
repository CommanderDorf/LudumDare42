using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;
    private Board _instance;
    
    [SerializeField] private int _boardSize = 10;
    [SerializeField] private List<GameObject> _tiles;
    [SerializeField] private List<GameObject> _coins;
    [SerializeField] private int _coinSpawnChance;
    [SerializeField] private AudioSource _coinAudio;
    [SerializeField] private GameObject _blocker;
    [SerializeField] private GameObject _border;
    private readonly int[] _rotations = new[] {0, 90, 180, 360};

    private List<Row> _rows = new List<Row>();
    private List<Row> _columns = new List<Row>();
    private GameObject[,] _blockers;

    private GameObject _boardObj;

    public int BoardSize
    {
        get { return _boardSize; }
    }

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
        _blockers = null;
    }

    public void NewBoard(int size)
    {
        _boardSize = size;
        BoardSetUp();
        SpawnBorders();
    }

    private void ResetBoard()
    {
        
        _rows.Clear();
        _columns.Clear();
        _blockers = null;

        foreach (Transform child in _boardObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    private void BoardSetUp()
    {
        ResetBoard();
        
        _blockers = new GameObject[_boardSize, _boardSize];
        
        for (int i = 0; i < _boardSize; i++)
        {
            _rows.Add(new Row(_boardSize, false));
            _columns.Add(new Row(_boardSize, true));
        }


        GameObject go;
        
        for (int x = 0; x < _boardSize; x++)
        {
            for (int y = 0; y < _boardSize; y++)
            {
                go = Instantiate(_tiles[Random.Range(0, _tiles.Count)],
                    new Vector3(x, y),
                    Quaternion.Euler(0, 0, _rotations[Random.Range(0, _rotations.Length)]));
                go.transform.parent = _boardObj.transform;
                
                _rows[y].Cells[x] = go;
                _columns[x].Cells[y] = go;

                
                // If we are on the start or goal tile, do not spawn a coin
                if ((x == 0 && y == 0) || (x == _boardSize - 1 && y == _boardSize - 1)) continue;
                
                // Chance to spawn a random coin
                if (_coinSpawnChance > Random.Range(0, 100))
                {
                    GameObject coin = Instantiate(_coins[Random.Range(0, _coins.Count)], go.transform.position, Quaternion.identity);
                    coin.transform.parent = go.transform;
                    coin.GetComponent<Coin>().SetAudio(_coinAudio);
                }
            }
        }
    }

    private void RefreshRow( bool isVertical)
    {
        for (int x = 0; x < _boardSize; x++)
        {
            for (int y = 0; y < _boardSize; y++)
            {
                if (isVertical)
                    _columns[x].Cells[y] = _rows[y].Cells[x];
                else
                {
                    _rows[y].Cells[x] = _columns[x].Cells[y];
                }
            }
        }
    }

    private void SpawnBorders()
    {
        GameObject currentGo = null;
        for (int x = -1; x < _boardSize + 1; x++)
        {
            for (int y = - 1; y < _boardSize + 1; y++)
            {
                if (x == -1 || x == _boardSize)
                    currentGo = Instantiate(_border, new Vector3(x, y), Quaternion.identity);
                else if(y == -1 || y == _boardSize)
                    currentGo = Instantiate(_border, new Vector3(x, y), Quaternion.identity);

                if (currentGo != null)
                {
                    currentGo.transform.parent = _boardObj.transform;
                }
            }
        }
    }
    
    public GameObject PushTile(GameObject tile, Vector2Int pos)
    {
        if (pos.x >= _boardSize && (pos.y < 0 || pos.y >= _boardSize)) return null;
        if (pos.x < 0 && (pos.y < 0 || pos.y >= _boardSize)) return null;

        tile.transform.parent = _boardObj.transform;
        
        // Right side of the board
        if (pos.x == _boardSize)
        {
            GameObject go = _rows[pos.y].PullRow(tile);
            RefreshRow(true);
            return go;
        }
        // Left side of the board
        else if (pos.x == -1)
        {
            GameObject go = _rows[pos.y].PushRow(tile);
            RefreshRow(true);
            return go;
        }
        
        // Top of the board
        if (pos.y == _boardSize)
        {
            GameObject go = _columns[pos.x].PullRow(tile);
            RefreshRow(false);
            return go;
        }
        // Bottom of the board
        else if (pos.y == -1)
        {
            GameObject go = _columns[pos.x].PushRow(tile);
            RefreshRow(false);
            return go;
        }

        return null;
    }

    public void BlockTile(int x, int y)
    {
        if (x == _boardSize - 1 && y == _boardSize - 1) return;
        GameObject blockerGo = Instantiate(_blocker, new Vector3(x, y, 0), Quaternion.identity);
        blockerGo.transform.parent = _boardObj.transform;
        _blockers[x, y] = blockerGo;
    }
}