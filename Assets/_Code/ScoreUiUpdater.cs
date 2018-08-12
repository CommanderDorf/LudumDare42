using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUiUpdater : MonoBehaviour
{
    private Text _scoreAmountText;

    private void Start()
    {
        _scoreAmountText = GetComponent<Text>();
    }

    private void Update()
    {
        _scoreAmountText.text = Score.ScoreAmount.ToString();
    }
}