using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private AudioSource _walkAudio;
    
    private RaycastHit2D _hit;

    public bool HasControl = false;
    
    // Update is called once per frame
    void Update()
    {
        if(!HasControl) return;;
        
        if (Input.GetButtonDown("Up"))
        {
            _hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, _wallMask);

            if (_hit.collider == null)
            {
                transform.Translate(Vector2.up);
                MoveCounter.MoveCount++;
                _walkAudio.Play();
            }
        }
        else if (Input.GetButtonDown("Down"))
        {
            _hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _wallMask);
            if (_hit.collider == null)
            {
                transform.Translate(Vector2.down);
                MoveCounter.MoveCount++;
                _walkAudio.Play();
            }
        }
        else if (Input.GetButtonDown("Right"))
        {
            _hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, _wallMask);
            if (_hit.collider == null)
            {
                transform.Translate(Vector2.right);
                MoveCounter.MoveCount++;
                _walkAudio.Play();
            }
        }
        else if (Input.GetButtonDown("Left"))
        {
            _hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, _wallMask);
            if (_hit.collider == null)
            {
                transform.Translate(Vector2.left);
                MoveCounter.MoveCount++;
                _walkAudio.Play();
            }
        }
    }
}