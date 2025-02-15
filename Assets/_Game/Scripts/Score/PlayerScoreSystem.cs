using IDosGames;
using System;
using System.Linq;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
	private const int DEFAULT_SCORE_VALUE = 0;
	private static string PLAYER_PREF_SCORE_RECORD = "SCORE_RECORD" + AuthService.UserID;
	private static string PLAYER_PREF_SCORE_CURRENT = "SCORE_CURRENT" + AuthService.UserID;

	public static event Action NewRecord;

	public static bool RecordBeated { get; private set; }

	public static int CurrentScore { get; private set; }

	public static int RecordScore { get; private set; }

	[SerializeField] private ScoreData _scoreData;
	[SerializeField] private PlayerScoreView _playerScoreView;

	private void OnEnable()
	{
		GameCubeSpawner.NewCubeSpawnedAfterMergeWithIndex += UpdateCurrentScoreWithCubeIndex;
		BombCube.CubeDestroyedByExplosionWithIndex += UpdateCurrentScoreWithCubeIndex;
		Playground.GameOver += OnGameOver;
		Playground.NewGame += OnGameOver;
	}

	private void OnDisable()
	{
		GameCubeSpawner.NewCubeSpawnedAfterMergeWithIndex -= UpdateCurrentScoreWithCubeIndex;
		BombCube.CubeDestroyedByExplosionWithIndex += UpdateCurrentScoreWithCubeIndex;
		Playground.GameOver -= OnGameOver;
		Playground.NewGame -= OnGameOver;
	}

	private void Start()
	{
		LoadScoresFromPlayerPrefs();
		UpdateScoreViews();
	}

	private void LoadScoresFromPlayerPrefs()
	{
		CurrentScore = PlayerPrefs.GetInt(PLAYER_PREF_SCORE_CURRENT, DEFAULT_SCORE_VALUE);
		RecordScore = PlayerPrefs.GetInt(PLAYER_PREF_SCORE_RECORD, DEFAULT_SCORE_VALUE);
	}

	private void UpdateScoreViews()
	{
		UpdateCurrentScoreView();
		UpdateRecordScoreView();
	}

	private void UpdateCurrentScoreView()
	{
		_playerScoreView.SetCurrentText(CurrentScore);
	}

	private void UpdateRecordScoreView()
	{
		_playerScoreView.SetRecordText(RecordScore);
	}

	private void UpdateCurrentScoreWithCubeIndex(CubeNumber.Index index)
	{
		var score = _scoreData.Scores.First(x => x.CubeIndex == index).Score;

		CurrentScore += score;

		UpdateCurrentScoreView();
		SetCurrentScorePlayerPrefs();

		if (CurrentScore > RecordScore)
		{
			RecordScore = CurrentScore;

			UpdateRecordScoreView();
			SetRecordScorePlayerPrefs();

			RecordBeated = true;
			NewRecord?.Invoke();
		}
		else
		{
			RecordBeated = false;
		}
	}

	private void SetCurrentScorePlayerPrefs()
	{
		PlayerPrefs.SetInt(PLAYER_PREF_SCORE_CURRENT, CurrentScore);
		PlayerPrefs.Save();
	}

	private void SetRecordScorePlayerPrefs()
	{
		PlayerPrefs.SetInt(PLAYER_PREF_SCORE_RECORD, RecordScore);
		PlayerPrefs.Save();
	}

	private void OnGameOver()
	{
		CurrentScore = 0;

		UpdateCurrentScoreView();
		SetCurrentScorePlayerPrefs();
	}
}
