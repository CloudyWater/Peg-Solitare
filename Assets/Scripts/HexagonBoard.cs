using UnityEngine;
using System.Collections;
using System;

public class HexagonBoard : Board
{
	public override void ClearSelections ()
	{
		throw new NotImplementedException ();
	}

	public override Hexagon GetHex (int xPosition, int yPosition)
	{
		throw new NotImplementedException ();
	}

	public override Jump GetJump (Hexagon selected, Hexagon target)
	{
		throw new NotImplementedException ();
	}

	public override bool HighlightPossibilities (Hexagon hex)
	{
		throw new NotImplementedException ();
	}

	public override bool IsGameOver (out int numPegsLeft)
	{
		throw new NotImplementedException ();
	}

	public override bool IsMovePossible (Hexagon hex)
	{
		throw new NotImplementedException ();
	}

	public override void RemoveHighlights ()
	{
		throw new NotImplementedException ();
	}

	public override void ResetBoard ()
	{
		throw new NotImplementedException ();
	}

	public override void SetUpBoard ()
	{
		throw new NotImplementedException ();
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}
}
