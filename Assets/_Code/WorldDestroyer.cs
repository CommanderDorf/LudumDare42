using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _blockingObj;
    

    private bool _firstRun = true;

    private bool[,] _blockers;
    private List<GameObject> _currentRunningWarnings = new List<GameObject>();
    
    private int _maxX;
    private int _maxY;
    
    public void StartBlocking(float blockingStartDelay, float blockingDelay)
    {
        
        _maxX = _maxY = Board.Instance.BoardSize;
        _blockers = new bool[_maxX, _maxY];
        StartCoroutine(Countdown(blockingStartDelay, blockingDelay));
    }

    public void StopBlocking()
    {
        StopAllCoroutines();

        foreach (GameObject go in _currentRunningWarnings)
        {
            Destroy(go);
        }
        
        _currentRunningWarnings.Clear();
    }

    // Create a Coroutine like in DangerHighlight and start spawning
    // DangerHighlight objects
    // TODO: Change DangerHighlight to a better name

    IEnumerator Countdown(float blockingStartDelay, float blockingDelay)
    {
        float currentTime = 0f;
        
        if (_firstRun)
        {
            while (currentTime < blockingStartDelay)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            _firstRun = false;
            currentTime = blockingDelay;
        }

        while (currentTime < blockingDelay)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        var posX = Random.Range(0, _maxX);
        var posY = Random.Range(0, _maxY);

        while (_blockers[posX, posY] || (posX == _maxX -1 && posY == _maxY - 1))
        {
            posX = Random.Range(0, _maxX);
            posY = Random.Range(0, _maxY);
            yield return null;
        }

        _currentRunningWarnings.Add(Instantiate(_blockingObj, new Vector3(posX, posY), Quaternion.identity));
        _blockers[posX, posY] = true;
        
        StartCoroutine(Countdown(blockingStartDelay, blockingDelay));
    }
}
