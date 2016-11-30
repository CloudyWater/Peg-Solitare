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
	private Hexagon mStartHex, mJumpedHex, mEndHex;

	//***************************************************************************
	// Function Name:	Jump
	// Purpose:				Constructor of the Jump Object.
	// Paramaters:		startHex	- The starting hex of the jump.
	//								jumpedHex	- The hex jumped over.
	//								endHex		- The ending hex of the jump.
	// Returns:				None
	//***************************************************************************
	public Jump (Hexagon startHex, Hexagon jumpedHex, Hexagon endHex)
	{
		mStartHex = startHex;
		mJumpedHex = jumpedHex;
		mEndHex = endHex;
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
}
