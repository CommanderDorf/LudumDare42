using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCountUpdater : MonoBehaviour
{
    private Text _moveCountText;

    private void Start()
    {
        _moveCountText = GetComponent<Text>();
    }

    private void Update()
    {
        _moveCountText.text = MoveCounter.MoveCount.ToString();
    }
}
