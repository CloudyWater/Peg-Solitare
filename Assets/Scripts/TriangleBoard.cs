//*****************************************************************************
// File Name:			TriangleBoard.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	Contains the TriangleBoard Board subclass. Triangle boards
//								have a special set of rules that can be applied to them.
//*****************************************************************************
using UnityEngine;
using System.Collections;
using System;

public class TriangleBoard : Board
{
	//***************************************************************************
	// Function Name:	Awake
	// Purpose:				Called upon Object Instantiation.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	protected override void Start ()
	{
	}

	//***************************************************************************
	// Function Name:	Update
	// Purpose:				Called once per frame. Calls back to Base class.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	protected override void Update ()
	{
		base.Update ();
	}

	//***************************************************************************
	// Function Name:	SetUpBoard
	// Purpose:				Instantiates all the hexes for a triangle board, and places
	//								them in the scene.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public override void SetUpBoard ()
	{
		mBoard = new Hexagon [mBoardSize, mBoardSize];
		GameObject rowBase, hexObject;
		Hexagon hexScript;

		for (int i = 0; i < mBoardSize; i++)
		{
			rowBase = GameObject.Instantiate (mHexagonPrefab);
			hexScript = rowBase.GetComponent<Hexagon> ();
			hexScript.Initiate (0, i, this);
			rowBase.transform.position = new Vector2 (-(i / 2.0f) * hexScript.GetHexWidth (),
				-i * hexScript.GetHexHeight () * (.75f));
			rowBase.transform.parent = this.transform;
			rowBase.name = "0," + i.ToString ();
			mBoard [0, i] = hexScript;

			for (int j = 1; j < i + 1; j++)
			{
				hexObject = GameObject.Instantiate (mHexagonPrefab);
				hexScript = hexObject.GetComponent<Hexagon> ();
				hexScript.Initiate (j, i, this);
				hexObject.transform.position = new Vector2 (rowBase.transform.position.x + j * hexScript.GetHexWidth (),
					rowBase.transform.position.y);
				hexObject.transform.parent = this.transform;
				hexObject.name = j.ToString () + "," + i.ToString ();
				mBoard [j, i] = hexScript;
			}
		}

		mBoard [mVacantPositionX, mVacantPositionY].EnablePeg (false);
	}

	//***************************************************************************
	// Function Name:	ResetBoard
	// Purpose:				Resets the state of the board to the initial state.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public override void ResetBoard ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Select (false);
				hex.Highlight (false);
				if (hex.GetXPosition () == mVacantPositionX && hex.GetYPosition () == mVacantPositionY)
				{
					hex.EnablePeg (false);
				}
				else
				{
					hex.EnablePeg (true);
				}
			}
		}
	}

	//***************************************************************************
	// Function Name:	HighlightPossibilities
	// Purpose:				Highlights all the possible moves from the passed in hex.
	// Paramaters:		hex							- The hex to check from.
	// Returns:				bMoveIsPossible - Whether or not there are possible moves.
	//***************************************************************************
	public override bool HighlightPossibilities (Hexagon hex)
	{
		bool bMoveIsPossible = false;

		// Up + Left
		if (hex.GetXPosition () - 1 >= 0 && hex.GetYPosition () - 1 >= 0 && mBoard [hex.GetXPosition () - 1,
			hex.GetYPosition () - 1].IsPegActive ())
		{
			if (hex.GetXPosition () - 2 >= 0 && hex.GetYPosition () - 2 >= 0 && !mBoard [hex.GetXPosition () - 2,
				hex.GetYPosition () - 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition () - 2, hex.GetYPosition () - 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Up + Right
		if (hex.GetYPosition () - 1 >= hex.GetXPosition () && mBoard [hex.GetXPosition (),
			hex.GetYPosition () - 1].IsPegActive ())
		{
			if (hex.GetYPosition () - 2 >= hex.GetXPosition () && !mBoard [hex.GetXPosition (),
				hex.GetYPosition () - 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition (), hex.GetYPosition () - 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Left
		if (hex.GetXPosition () - 1 >= 0 && mBoard [hex.GetXPosition () - 1, hex.GetYPosition ()].IsPegActive ())
		{
			if (hex.GetXPosition () - 2 >= 0 && !mBoard [hex.GetXPosition () - 2, hex.GetYPosition ()].IsPegActive ())
			{
				mBoard [hex.GetXPosition () - 2, hex.GetYPosition ()].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Right
		if (hex.GetXPosition () + 1 <= hex.GetYPosition () && mBoard [hex.GetXPosition () + 1,
			hex.GetYPosition ()].IsPegActive ())
		{
			if (hex.GetXPosition () + 2 <= hex.GetYPosition () && !mBoard [hex.GetXPosition () + 2,
				hex.GetYPosition ()].IsPegActive ())
			{
				mBoard [hex.GetXPosition () + 2, hex.GetYPosition ()].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Down + Left
		if (hex.GetYPosition () + 1 < mBoardSize && mBoard [hex.GetXPosition (), hex.GetYPosition () + 1].IsPegActive ())
		{
			if (hex.GetYPosition () + 2 < mBoardSize && !mBoard [hex.GetXPosition (), hex.GetYPosition () + 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition (), hex.GetYPosition () + 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Down + Right
		if (hex.GetYPosition () + 1 < mBoardSize && hex.GetXPosition () + 1 < mBoardSize && mBoard [hex.GetXPosition () + 1,
			hex.GetYPosition () + 1].IsPegActive ())
		{
			if (hex.GetYPosition () + 2 < mBoardSize && hex.GetXPosition () + 2 < mBoardSize && !mBoard [hex.GetXPosition () + 2,
				hex.GetYPosition () + 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition () + 2, hex.GetYPosition () + 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		return bMoveIsPossible;
	}

}

