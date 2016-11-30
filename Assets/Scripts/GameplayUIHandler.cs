using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameplayUIHandler : MonoBehaviour
{
	private const string MAIN_MENU = "MainMenu";
	private const string GAME_OVER_TEXT = "Game Over, ";
	private const string WIN_TEXT = "You Win!";
	private const string LOSS_TEXT = "You Lose!";
	private const string REMAINING_TIME_TEXT = "Remaining Time: ";
	private const int START_TIME = 180;

	public Board mBoard;
	public GameObject mGameOverPanel;
	public Text mWinLossText;
	public Text mRemainingTimeText;
	public Text mTimerText;

	private bool mbIsGameOver;
	private int mTimeLeft;
	private float mSecondTimer;

	// Use this for initialization
	void Start ()
	{
		mTimeLeft = START_TIME;
		mbIsGameOver = false;
		mGameOverPanel.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (!mbIsGameOver)
		{
			mSecondTimer += Time.deltaTime;
			if (mSecondTimer > 1)
			{
				mSecondTimer -= 1;
				mTimeLeft -= 1;
			}

			string timerText = FormatRemainingTime ();

			mTimerText.text = timerText;

			if (mTimeLeft <= 0)
			{
				GameOver (false);
			}
		}
	}

	public void GameOver (bool bWin)
	{
		mGameOverPanel.SetActive (true);
		mTimerText.gameObject.SetActive (false);
		mbIsGameOver = true;

		if (bWin)
		{
			mWinLossText.text = GAME_OVER_TEXT + WIN_TEXT;
		}
		else
		{
			mWinLossText.text = GAME_OVER_TEXT + LOSS_TEXT;
		}

		mRemainingTimeText.text = REMAINING_TIME_TEXT + FormatRemainingTime ();
	}

	public string FormatRemainingTime ()
	{
		return string.Format ("{0}:{1:D2}", mTimeLeft / 60, (mTimeLeft % 60));
	}

	public void ResetLevel ()
	{
		mSecondTimer = 0;
		mTimeLeft = START_TIME;

		mGameOverPanel.SetActive (false);
		mTimerText.gameObject.SetActive (true);
		mbIsGameOver = false;
		mBoard.ResetBoard ();
	}

	public void Quit ()
	{
		SceneManager.LoadScene (MAIN_MENU);
	}

	public bool IsGameOver ()
	{
		return mbIsGameOver;
	}
}
