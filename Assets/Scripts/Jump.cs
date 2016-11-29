using System;
using System.Collections.Generic;

public class Jump
{
	private Hexagon mStartHex, mJumpedHex, mEndHex;

	public Jump (Hexagon startHex, Hexagon jumpedHex, Hexagon endHex)
	{
		mStartHex = startHex;
		mJumpedHex = jumpedHex;
		mEndHex = endHex;
	}

	public Hexagon GetStartHex ()
	{
		return mStartHex;
	}

	public Hexagon GetJumpedHex ()
	{
		return mJumpedHex;
	}

	public Hexagon GetEndHex ()
	{
		return mEndHex;
	}
}
