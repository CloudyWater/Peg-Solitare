//*****************************************************************************
// File Name:			Hexagon.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	Each Hexagon is one space on the board. This class contains
//								methods to select or highlight the space, as well as member
//								variables containing space information.
//*****************************************************************************
using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour
{
	public float SELECTION_SCALE = 1.2f;

	public SpriteRenderer mBase, mHole, mPeg;
	public Color mHighlightColor, mNormalHoleColor, mNormalPegColor;

	private Board mBoard;
	private bool mbIsPegActive, mbIsHighlighted;
	private int mXposition, mYposition;

	//***************************************************************************
	// Function Name:	Start
	// Purpose:				Sets up the Hexagon.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	void Start ()
	{
		mHole.color = mNormalHoleColor;
	}

	//***************************************************************************
	// Function Name:	Initiate
	// Purpose:				Sets the hexagon's position in the board.
	// Paramaters:		xPosition - The x position of the hexagon.
	//								yPosition - The y position of the hexagon.
	//								board			- The board the hexagon is on.
	// Returns:				None
	//***************************************************************************
	public void Initiate (int xPosition, int yPosition, Board board)
	{
		mXposition = xPosition;
		mYposition = yPosition;
		mBoard = board;
		EnablePeg (true);
		Highlight (false);
	}

	void Update ()
	{

	}

	//***************************************************************************
	// Function Name:	EnablePeg
	// Purpose:				Sets whether there is a peg in the hole of this space or not.
	// Paramaters:		bEnabled - true if a peg is there, false if not.
	// Returns:				None
	//***************************************************************************
	public void EnablePeg (bool bEnabled)
	{
		mPeg.enabled = bEnabled;
		mbIsPegActive = bEnabled;
	}

	//***************************************************************************
	// Function Name:	IsPegActive
	// Purpose:				Returns whether or not the peg is there.
	// Paramaters:		None
	// Returns:				mbIsPegActive - Is there an active peg in this hex.
	//***************************************************************************
	public bool IsPegActive ()
	{
		return mbIsPegActive;
	}

	//***************************************************************************
	// Function Name:	Highlight
	// Purpose:				Sets whether or not the hex is highlighted.
	// Paramaters:		bIsHighlighted - If true, highlits the hex, if false removes
	//								highlight.
	// Returns:				None
	//***************************************************************************
	public void Highlight (bool bIsHighlighted)
	{
		mbIsHighlighted = bIsHighlighted;
		if (bIsHighlighted)
		{
			mHole.color = mHighlightColor;
		}
		else
		{
			mHole.color = mNormalHoleColor;
		}
	}

	//***************************************************************************
	// Function Name:	IsHighlighted
	// Purpose:				Returns whether the hex is highlighted.
	// Paramaters:		None
	// Returns:				mbIsHighlighted - Whether the hex is highlighted
	//***************************************************************************
	public bool IsHighlighted ()
	{
		return mbIsHighlighted;
	}

	//***************************************************************************
	// Function Name:	Select
	// Purpose:				Selects the hex, removing the peg, or deselects it, replacing
	//								the peg, based on the passed in value.
	// Paramaters:		bIsSelected - Whether to remove or replace the peg.
	// Returns:				None
	//***************************************************************************
	public void Select (bool bIsSelected)
	{
		if (bIsSelected)
		{
			mPeg.transform.localScale = new Vector3 (SELECTION_SCALE, SELECTION_SCALE, 1);
			mPeg.color = mHighlightColor;
		}
		else
		{
			mPeg.transform.localScale = new Vector3 (1, 1, 1);
			mPeg.color = mNormalPegColor;
		}
	}

	//***************************************************************************
	// Function Name:	GetXPosition
	// Purpose:				Getter for mXPosition.
	// Paramaters:		None
	// Returns:				mXPosition - The x position of the hex within the board.
	//***************************************************************************
	public int GetXPosition ()
	{
		return mXposition;
	}

	//***************************************************************************
	// Function Name:	GetYPosition
	// Purpose:				Getter for mYPosition
	// Paramaters:		None
	// Returns:				mYPosition - The y position of the hex within the board.
	//***************************************************************************
	public int GetYPosition ()
	{
		return mYposition;
	}

	//***************************************************************************
	// Function Name:	GetHexHeight
	// Purpose:				Returns the vertical size of the hex's sprite in Unity units.
	// Paramaters:		None
	// Returns:				The vertical size of the hex's sprite.
	//***************************************************************************
	public float GetHexHeight ()
	{
		return mBase.bounds.size.y;
	}

	//***************************************************************************
	// Function Name:	GetHexWidth
	// Purpose:				Returns the horizontal size of the hex's sprite in Unity units.
	// Paramaters:		None
	// Returns:				The horizontal size of the hex's sprite.
	//***************************************************************************
	public float GetHexWidth ()
	{
		return mBase.bounds.size.x;
	}
}
