using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerHighlight : MonoBehaviour
{
    private float _duration = 5f;
    private float _startSpeed = 1f;
    private float _endSpeed = 4f;

    private Animator _animator;
    
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Triggered", true);
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        float currentTime = 0f;

        while (currentTime < _duration)
        {
            _animator.speed = Mathf.Lerp(_startSpeed, _endSpeed, currentTime / _duration);
            
            currentTime += Time.deltaTime;

            yield return null;
        }
        
        // Call function to handle placing the block
        _animator.SetBool("Triggered", false);
        
        Board.Instance.BlockTile((int)transform.position.x, (int)transform.position.y);
        
        // TODO: Add object pooling
        Destroy(gameObject, 3f);
    }
}