//*****************************************************************************
// File Name:			Board.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	The Abstract Board class. Contains methods to set up the board,
//								reset the board, clear all board selections, clear all board
//								highlights, highlight all possible moves from one hex, check
//								if a hex has possible moves, check if the game is over, and
//								calculate a Jump from two hex endpoints.
//*****************************************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Board : MonoBehaviour
{
	public static string mLevelName;
	public static int mBoardSize;
	public static int mVacantPositionX, mVacantPositionY;
	public GameObject mHexagonPrefab;

	protected Hexagon [,] mBoard;

	protected virtual void Start ()
	{

	}

	protected virtual void Update ()
	{

	}

	public abstract void SetUpBoard ();

	public abstract void ResetBoard ();

	public abstract bool HighlightPossibilities (Hexagon hex);

	//***************************************************************************
	// Function Name:	IsMovePossible
	// Purpose:				Checks if there are any moves starting at the passed in hex.
	// Paramaters:		hex						- The hexagon to check for possible moves.
	// Returns:				mbIsPossible	- Whether there are any moves
	//***************************************************************************
	public virtual bool IsMovePossible (Hexagon hex)
	{
		bool mbIsPossible = false;
		mbIsPossible = HighlightPossibilities (hex);
		RemoveHighlights ();
		return mbIsPossible;
	}

	//***************************************************************************
	// Function Name:	IsGameOver
	// Purpose:				Checks if the game is over.
	// Paramaters:		out numPegsLeft - returns the number of remaining pegs.
	// Returns:				bIsGameOver			- Whether or not the game is over.
	//***************************************************************************
	public virtual bool IsGameOver (out int numPegsLeft)
	{
		bool bIsGameOver = true;
		numPegsLeft = 0;
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null && hex.IsPegActive ())
			{
				if (IsMovePossible (hex))
				{
					bIsGameOver = false;
				}
				numPegsLeft++;
			}
		}

		return bIsGameOver;
	}

	//***************************************************************************
	// Function Name:	GetJump
	// Purpose:				Creates a Jump based upon a starting hexagon and an ending
	//								hexagon.
	// Paramaters:		selected	- the starting hexagon.
	//								target		- the ending hexagon.
	// Returns:				retJump		- the resulting Jump.
	//***************************************************************************
	public virtual Jump GetJump (Hexagon selected, Hexagon target)
	{
		Jump retJump = null;
		int jumpedXPosition = selected.GetXPosition ();
		int jumpedYPosition = selected.GetYPosition ();

		if (selected.GetXPosition () > target.GetXPosition ())
		{
			jumpedXPosition = selected.GetXPosition () - 1;
		}
		else if (selected.GetXPosition () < target.GetXPosition ())
		{
			jumpedXPosition = selected.GetXPosition () + 1;
		}

		if (selected.GetYPosition () > target.GetYPosition ())
		{
			jumpedYPosition = selected.GetYPosition () - 1;
		}
		else if (selected.GetYPosition () < target.GetYPosition ())
		{
			jumpedYPosition = selected.GetYPosition () + 1;
		}

		Hexagon jumpedHex = mBoard [jumpedXPosition, jumpedYPosition];

		retJump = new Jump (selected, jumpedHex, target, GameplayUIHandler.GetRemainingTime ());

		return retJump;
	}

	//***************************************************************************
	// Function Name:	ClearSelections
	// Purpose:				Clears all selected pegs.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public virtual void ClearSelections ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Select (false);
			}
		}
	}

	//***************************************************************************
	// Function Name:	RemoveHighlights
	// Purpose:				Sets all hexes to not be highlighted.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public virtual void RemoveHighlights ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Highlight (false);
			}
		}
	}

	//***************************************************************************
	// Function Name:	GetHex
	// Purpose:				Returns the hex at the passed in co-ordinates.
	// Paramaters:		xPosition - The x Position of the hex to get.
	//								yPosition - The y Position of the hex to get.
	// Returns:				The hexagon at the desired position.
	//***************************************************************************
	public virtual Hexagon GetHex (int xPosition, int yPosition)
	{
		return mBoard [xPosition, yPosition];
	}
}
