using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private PlayerController _player;
	[SerializeField] private GameObject _goal;
	[SerializeField] private Board _boardController;
	[SerializeField] private WorldDestroyer _worldBlocker;
	[SerializeField] private MouseController _mouseController;

	[SerializeField] private float _blockingStartDelay;
	[SerializeField] private float _blockingDelay;
	[SerializeField] private UIController _uiController;

	[SerializeField] private int _startingDifficulty;
	[SerializeField] private int _levelsBetweenDifficulties;

	[SerializeField] private AudioSource _deathAudio;

	private int _currentDifficulty;
	private int _currentLevel;
	
	private bool _readyForNextLevel = true;
	private bool _gameOver = false;
	
	public static GameController Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		_currentDifficulty = _startingDifficulty - 1;
		_currentLevel = 1;
	}

	private void Start()
	{
		NextLevel();
	}

	public void GoalReached()
	{
		_goal.SetActive(false);
		_worldBlocker.StopBlocking();
		_player.gameObject.SetActive(false);
		_player.HasControl = false;
		_mouseController.PlayerHasControl = false;

		Score.ScoreAmount += 100;
		NextLevel();
	}

	public void NextLevel()
	{
		_readyForNextLevel = false;
		_uiController.DisableGameOverUI();

		int nextDifficulty = _currentDifficulty;
		
		if (_currentLevel %_levelsBetweenDifficulties == 0)
		{
			nextDifficulty++;
			_currentDifficulty = nextDifficulty;
			_blockingStartDelay -= 0.2f;
			_blockingDelay -= 0.3f;
		}
		else
		{
			_currentLevel++;
		}

		if (nextDifficulty > 15)
		{
			nextDifficulty = 15;
			_blockingStartDelay -= 0.1f;
			
			Mathf.Clamp(_blockingDelay -= 0.3f, 1, 10);
		}
		
		LoadLevel(nextDifficulty);
		StartLevel();
	}

	public void PlayedIsDead()
	{
		_deathAudio.Play();
		_uiController.EnableDiedText();
		_goal.SetActive(false);
		_worldBlocker.StopBlocking();
		_player.gameObject.SetActive(false);
		_player.HasControl = false;
		_mouseController.PlayerHasControl = false;
		StartCoroutine(ScoreTally());
	}

	private void GameOver()
	{
		_uiController.EnableGameOverUI();
		_uiController.DisableDiedText();
		_gameOver = true;
	}

	private void StartLevel()
	{
		_worldBlocker.StartBlocking(_blockingStartDelay, _blockingDelay);
	}

	private void LoadLevel(int size)
	{
		// Load up a board
		_boardController.NewBoard(size);
		
		// Setup a camera (boardzie / 2 - .5)
		float midPoint = ((float)size / 2) - 0.5f;
		Camera.main.transform.position = new Vector3(midPoint, midPoint + 0.5f, -10f);
		Camera.main.orthographicSize = ((float)size + 3) / 2;
		
		// Activate goal and move it
		_goal.transform.position = new Vector2(size - 1, size - 1);
		_goal.SetActive(true);

		// Reset player position and activate player
		_player.transform.position = Vector2.zero;
		_player.gameObject.SetActive(true);
		_player.HasControl = true;

		_mouseController.PlayerHasControl = true;
		
		_uiController.EnableScoreUI();
	}


	private void Update()
	{
		if (_readyForNextLevel && Input.GetKeyDown(KeyCode.Space))
		{
			NextLevel();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && _gameOver)
		{
			SceneController.Instance.RestartGame();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			PlayedIsDead();
		}
	}

	IEnumerator ScoreTally()
	{
		while (MoveCounter.MoveCount > 0)
		{
			if (MoveCounter.MoveCount % 10 == 0)
			{
				Score.ScoreAmount -= 10;
			}
			MoveCounter.MoveCount--;
			yield return new WaitForSeconds(0.05f);
		}
		
		yield return new WaitForSeconds(2);
		
		GameOver();
	}
}