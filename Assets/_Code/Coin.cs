using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private int _scoreAmount;
	private AudioSource _coinAudio;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Score.ScoreAmount += _scoreAmount;
			_coinAudio.Play();
			Destroy(gameObject);
		}
	}

	public void SetAudio(AudioSource coinAudio)
	{
		_coinAudio = coinAudio;
	}
}