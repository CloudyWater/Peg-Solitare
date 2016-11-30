//*****************************************************************************
// File Name:			GameplayUIHandler.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	This class handles the Gameplay UI. This includes setting the
//								timer, keeping track of remaining time, displaying remaining
//								time, and displaying the Game Over UI.
//*****************************************************************************
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

	//***************************************************************************
	// Function Name:	Start
	// Purpose:				Sets the timer to the max time of 3 minutes, and stops the
	//								Game Over panel from being displayed.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	void Start ()
	{
		mTimeLeft = START_TIME;
		mbIsGameOver = false;
		mGameOverPanel.SetActive (false);
	}

	//***************************************************************************
	// Function Name:	Update
	// Purpose:				Updates the game timer and displays the remaining time.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
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

	//***************************************************************************
	// Function Name:	GameOver
	// Purpose:				Displays the Game Over UI.
	// Paramaters:		bWin - Whether the game was won or lost.
	// Returns:				None
	//***************************************************************************
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

	//***************************************************************************
	// Function Name:	FormatRemainingTime
	// Purpose:				Formats the remaining time to minutes:seconds.
	// Paramaters:		None
	// Returns:				string value containing formatted remaining time.
	//***************************************************************************
	public string FormatRemainingTime ()
	{
		return string.Format ("{0}:{1:D2}", mTimeLeft / 60, (mTimeLeft % 60));
	}

	//***************************************************************************
	// Function Name:	ResetLevel
	// Purpose:				Resets the entire level.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public void ResetLevel ()
	{
		mSecondTimer = 0;
		mTimeLeft = START_TIME;

		mGameOverPanel.SetActive (false);
		mTimerText.gameObject.SetActive (true);
		mbIsGameOver = false;
		mBoard.ResetBoard ();
	}

	//***************************************************************************
	// Function Name:	Quit
	// Purpose:				Exits to main menu.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public void Quit ()
	{
		SceneManager.LoadScene (MAIN_MENU);
	}

	//***************************************************************************
	// Function Name:	IsGameOver
	// Purpose:				Getter for mbIsGameOver.
	// Paramaters:		None
	// Returns:				mbIsGameOver - Changes based on whether the game is finished.
	//***************************************************************************
	public bool IsGameOver ()
	{
		return mbIsGameOver;
	}
}
