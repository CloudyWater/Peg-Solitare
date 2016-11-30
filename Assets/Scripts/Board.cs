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

	public GameObject mHexagonPrefab;

	protected virtual void Awake ()
	{
		SetUpBoard ();
	}

	protected virtual void Update ()
	{

	}

	protected abstract void SetUpBoard ();

	public abstract void ResetBoard ();

	public abstract void ClearSelections ();

	public abstract bool IsMovePossible (Hexagon hex);

	public abstract bool HighlightPossibilities (Hexagon hex);

	public abstract Jump GetJump (Hexagon selected, Hexagon target);

	public abstract bool IsGameOver (out int numPegsLeft);

	public abstract void RemoveHighlights ();
}
