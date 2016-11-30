//*****************************************************************************
// File Name:			Jump.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	Each Jump is one action taken on the board. A jump contains
//								information about which hex the jump originated in, moved
//								over, and ended in.
//*****************************************************************************
using System;
using System.Collections.Generic;

public class Jump
{
	[Serializable]
	public struct SaveData
	{
		public int mStartX, mStartY;
		public int mJumpedX, mJumpedY;
		public int mEndX, mEndY;
		public int mTimeLeft;
	}

	private Hexagon mStartHex, mJumpedHex, mEndHex;
	private int mRemainingTime;

	//***************************************************************************
	// Function Name:	Jump
	// Purpose:				Constructor of the Jump Object.
	// Paramaters:		startHex	- The starting hex of the jump.
	//								jumpedHex	- The hex jumped over.
	//								endHex		- The ending hex of the jump.
	// Returns:				None
	//***************************************************************************
	public Jump (Hexagon startHex, Hexagon jumpedHex, Hexagon endHex, int remainingTime)
	{
		mStartHex = startHex;
		mJumpedHex = jumpedHex;
		mEndHex = endHex;
		mRemainingTime = remainingTime;
	}

	//***************************************************************************
	// Function Name:	GetStartHex
	// Purpose:				Returns the starting hex of the jump.
	// Paramaters:		None
	// Returns:				mStartHex - The starting hex of the jump.
	//***************************************************************************
	public Hexagon GetStartHex ()
	{
		return mStartHex;
	}

	//***************************************************************************
	// Function Name:	GetJumpedHex
	// Purpose:				Returns the Jumped hex of the jump.
	// Paramaters:		None
	// Returns:				mJumpedHex - The jumped hex of the jump.
	//***************************************************************************
	public Hexagon GetJumpedHex ()
	{
		return mJumpedHex;
	}

	//***************************************************************************
	// Function Name:	GetEndHex
	// Purpose:				Returns the ending hex of the jump.
	// Paramaters:		None
	// Returns:				mEndHex - The ending hex of the jump.
	//***************************************************************************
	public Hexagon GetEndHex ()
	{
		return mEndHex;
	}

	//***************************************************************************
	// Function Name:	GetRemainingTime
	// Purpose:				Returns the time left at the time of the jump
	// Paramaters:		None
	// Returns:				mRemainingTime - Time remaining after jump.
	//***************************************************************************
	public int GetRemainingTime ()
	{
		return mRemainingTime;
	}

	//***************************************************************************
	// Function Name:	GetSaveData
	// Purpose:				Returns a SaveData struct with all the needed data
	// Paramaters:		None
	// Returns:				retData - Save data struct, filled with Jump data.
	//***************************************************************************
	public SaveData GetSaveData ()
	{
		SaveData retData = new SaveData ();
		retData.mStartX = mStartHex.GetXPosition ();
		retData.mStartY = mStartHex.GetYPosition ();
		retData.mJumpedX = mJumpedHex.GetXPosition ();
		retData.mJumpedY = mJumpedHex.GetYPosition ();
		retData.mEndX = mEndHex.GetXPosition ();
		retData.mEndY = mEndHex.GetYPosition ();
		retData.mTimeLeft = mRemainingTime;

		return retData;
	}

	//***************************************************************************
	// Function Name:	CreateFromData
	// Purpose:				Creates a Jump from a SaveData struct
	// Paramaters:		None
	// Returns:				Jump - The created Jump.
	//***************************************************************************
	public static Jump CreateFromData (Board board, SaveData data)
	{
		Hexagon startHex = board.GetHex (data.mStartX, data.mStartY);
		Hexagon jumpedHex = board.GetHex (data.mJumpedX, data.mJumpedY);
		Hexagon endHex = board.GetHex (data.mEndX, data.mEndY);
		Jump retJump = new Jump (startHex, jumpedHex, endHex, data.mTimeLeft);

		return retJump;
	}
}
