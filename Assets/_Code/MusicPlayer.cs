using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer _musicPlayer = null;
    
    private AudioSource _musicSource;
    private bool _isPlaying = true;


    private void Awake()
    {
        if (_musicPlayer == null)
        {
            DontDestroyOnLoad(gameObject);
            _musicPlayer = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_isPlaying)
            {
                _isPlaying = false;
                _musicSource.volume = 0;
            }
            else
            {
                _isPlaying = true;
                _musicSource.volume = 1;
            }
        }
    }
}