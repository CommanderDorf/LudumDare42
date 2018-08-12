using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    enum SceneState
    {
        Menu,
        Tutorial,
        Game
    }

    private SceneState _state = SceneState.Menu;

    public static SceneController Instance = null;
    
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (_state == SceneState.Game) return; 
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_state == SceneState.Game)
            {
                _state = SceneState.Menu;
            }
            else
            {
                _state++;
            }
            
            SceneManager.LoadScene((int)_state);
        }
    }

    public void RestartGame()
    {
        _state = SceneState.Menu;
        SceneManager.LoadScene(0);
    }
}
