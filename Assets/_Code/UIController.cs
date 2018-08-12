using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas _scoreCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Text _diedText;

    public void EnableScoreUI()
    {
        _scoreCanvas.gameObject.SetActive(true);
    }
    
    public void DisableScoreUI()
    {
        _scoreCanvas.gameObject.SetActive(false);
    }

    public void EnableGameOverUI()
    {
        _gameOverCanvas.gameObject.SetActive(true);
    }

    public void DisableGameOverUI()
    {
        _gameOverCanvas.gameObject.SetActive(false);
    }

    public void EnableDiedText()
    {
        _diedText.enabled = true;
    }

    public void DisableDiedText()
    {
        _diedText.enabled = false;
    }
}