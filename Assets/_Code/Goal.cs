using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;


    private void OnTriggerEnter2D(Collider2D other)
    {
        _particles.Play();
        GameController.Instance.GoalReached();
    }
}