//*****************************************************************************
// File Name:			Move.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	Moves store a queue of jumps that must be completed for the
//								Move to be finished. This class is used to store moves in a
//								game for the replay system.
//*****************************************************************************
using System;
using System.Collections.Generic;

public class Move
{
	private Queue<Jump> mJumps;

	//***************************************************************************
	// Function Name:	Move
	// Purpose:				Object Contructor. Sets up mJumps.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public Move ()
	{
		mJumps = new Queue<Jump> ();
	}

	//***************************************************************************
	// Function Name:	AddJump
	// Purpose:				Adds a Jump to the Move.
	// Paramaters:		jump - The jump to add to mJumps.
	// Returns:				None
	//***************************************************************************
	public void AddJump (Jump jump)
	{
		mJumps.Enqueue (jump);
	}
}

