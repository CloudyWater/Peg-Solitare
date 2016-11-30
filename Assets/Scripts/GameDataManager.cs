//*****************************************************************************
// File Name:			GameDataManager.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	This class manages all save data for the game. This includes
//								keeping track of moves made during the game to save them after
//								the game is complete as well as loading saved games.
//*****************************************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameDataManager
{
	public const string TRIANGLE = "Triangle";
	public const string CUSTOM = "Custom";
	private const string SAVE_DIRECTORY = "\\SaveGames\\";
	private const string SAVE_LOCATION = "save.sav";

	public static readonly GameDataManager mSingleton = new GameDataManager ();

	private static Queue<Jump> mGameJumps;
	private static Jump[] mLoadedGame;
	private int mCurrentJump;

	//***************************************************************************
	// Function Name:	GameDataManager
	// Purpose:				Private constructor. Sets up the mGameJumps queue.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	private GameDataManager ()
	{
		mGameJumps = new Queue<Jump> ();
	}

	//***************************************************************************
	// Function Name:	AddJump
	// Purpose:				Adds a jump to the queue of jumps.
	// Paramaters:		jump - the jump to add to the queue.
	// Returns:				None
	//***************************************************************************
	public void AddJump (Jump jump)
	{
		mGameJumps.Enqueue (jump);
	}

	//***************************************************************************
	// Function Name:	Start
	// Purpose:				Writes all the jumps in the mGameJumps queue to the save
	//								file. 
	// Paramaters:		boardType - String containing the type of board.
	//								boardSize - The size of the board array.
	// Returns:				None
	//***************************************************************************
	public void WriteGameToFile (string boardType, int boardSize)
	{
		Jump currentJump;
		Directory.CreateDirectory (Application.persistentDataPath + SAVE_DIRECTORY);
		using (StreamWriter writer = File.CreateText (Application.persistentDataPath + SAVE_DIRECTORY + SAVE_LOCATION))
		{
			// Adds the type of board, the size of the board, and the number of jumps as the first line
			writer.WriteLine (boardType + " " + boardSize.ToString () + " " + mGameJumps.Count);

			while (mGameJumps.Count > 0)
			{
				currentJump = mGameJumps.Dequeue ();
				Jump.SaveData data = currentJump.GetSaveData ();
				// Formats each jump as "startx starty,jumpedx jumpedy, endx endy, timeleft"
				writer.WriteLine (string.Format ("{0} {1},{2} {3},{4} {5}, {6}", data.mStartX, data.mStartY, data.mJumpedX,
					data.mJumpedY, data.mEndX, data.mEndY, data.mTimeLeft));
			}
		}
		Debug.Log ("Save file created at: " + Application.persistentDataPath + SAVE_DIRECTORY + SAVE_LOCATION);
	}

	//***************************************************************************
	// Function Name:	LoadGameFromFile
	// Purpose:				Loads a game from the saved game file.
	// Paramaters:		boardObject - The object which has the board script attached
	// Returns:				None
	//***************************************************************************
	public void LoadGameFromFile (GameObject boardObject)
	{
		char [] delimiters = { ' ', ',' };
		string currentLine;
		string [] splitGameType;
		string [] splitJumpData;
		Jump loadedJump;
		Jump.SaveData data;
		Board board = null;
		using (StreamReader reader = File.OpenText (Application.persistentDataPath + SAVE_DIRECTORY + SAVE_LOCATION))
		{
			currentLine = reader.ReadLine ();
			splitGameType = currentLine.Split (delimiters, System.StringSplitOptions.RemoveEmptyEntries);
			if (string.Equals (splitGameType [0], TRIANGLE))
			{
				Board.mBoardSize = int.Parse (splitGameType [1]);
				if (Board.mBoardSize == 4)
				{
					TriangleBoard.mVacantPositionX = 0;
					TriangleBoard.mVacantPositionY = 1;
				}
				else
				{
					TriangleBoard.mVacantPositionX = TriangleBoard.mVacantPositionY = 0;
				}
				board = boardObject.GetComponent<TriangleBoard> ();
				board.SetUpBoard ();
				boardObject.GetComponent<HexagonBoard> ().enabled = false;

				mLoadedGame = new Jump [int.Parse (splitGameType [2])];
			}
			else if (string.Equals (splitGameType [0], CUSTOM))
			{
				// Load the custom board!
			}

			currentLine = reader.ReadLine ();
			mCurrentJump = 0;
			while (currentLine != null && !currentLine.Equals (""))
			{
				splitJumpData = currentLine.Split (delimiters, System.StringSplitOptions.RemoveEmptyEntries);
				data = new Jump.SaveData ();
				data.mStartX = int.Parse (splitJumpData [0]);
				data.mStartY = int.Parse (splitJumpData [1]);
				data.mJumpedX = int.Parse (splitJumpData [2]);
				data.mJumpedY = int.Parse (splitJumpData [3]);
				data.mEndX = int.Parse (splitJumpData [4]);
				data.mEndY = int.Parse (splitJumpData [5]);
				data.mTimeLeft = int.Parse (splitJumpData [6]);

				loadedJump = Jump.CreateFromData (board, data);

				mLoadedGame [mCurrentJump++] = loadedJump;

				currentLine = reader.ReadLine ();
			}
			mCurrentJump = 0;
		}
	}

	//***************************************************************************
	// Function Name:	GetNextMove
	// Purpose:				Returns the next jump in the game's sequence. SHOULD NOT
	//								BE CALLED UNLESS AFTER LoadGameFromFile.
	// Paramaters:		None
	// Returns:				retJump - The next jump from the loaded file.
	//***************************************************************************
	public Jump GetNextMove ()
	{
		Jump retJump = null;
		if (mCurrentJump < mLoadedGame.Length)
		{
			retJump = mLoadedGame [mCurrentJump++];
		}
		return retJump;
	}

	//***************************************************************************
	// Function Name:	GetPreviousMove
	// Purpose:				Returns the previous jump in the game's sequence. SHOULD NOT
	//								BE CALLED UNLESS AFTER LoadGameFromFile.
	// Paramaters:		None
	// Returns:				retJump - the move before the last jump.
	//***************************************************************************
	public Jump GetPreviousMove ()
	{
		Jump retJump = null;
		if (mCurrentJump > 0)
		{
			retJump = mLoadedGame [--mCurrentJump];
		}
		return retJump;
	}

}

