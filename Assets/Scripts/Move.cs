using System;
using System.Collections.Generic;

public class Move
{
	private Queue<Jump> mJumps;

	public Move ()
	{
		mJumps = new Queue<Jump> ();
	}

	public void AddJump (Jump jump)
	{
		mJumps.Enqueue (jump);
	}
}

