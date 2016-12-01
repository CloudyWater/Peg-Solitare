//*****************************************************************************
// File Name:			InputHandler.cs
// File Author:		Thomas Hyman
// Date:					11/29/16
// File Purpose:	Handles all input. The current input flow is as follows:
//								1.	Player mouses over / taps hex, hex is highlighted if move
//										is possible.
//								2.	Player clicks / double taps hex, hex is selected. All
//										possible moves starting from that hex are highlighted.
//								3a.	Player clicks / taps another hex, goto 1 / 2.
//								3b.	Player clicks / taps outside of hexes, deselect hex.
//								3c.	Player clicks / taps highlighted hole, move completes.
//*****************************************************************************
//TODO: create InputHandler base class, subclass TouchInputHandler and MouseInputHandler.
//			Instantiate only one based on the device it loads on?
//			State Machine for Input

using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public const string HEXAGON_LAYER = "Hexagons";
	public const float DOUBLE_TAP_LENGTH = 0.5f;

	public Camera mMainCamera;
	public Board mBoard;
	public GameplayUIHandler mGameplayUI;

	private Hexagon mSelectedHexagon, mTargetHexagon;
	private float mDoubleTapTimer;
	private bool mbDoubleTapAvailable, mbSelectionLocked;

	private enum BoardState
	{
		None, Hover, Selecting, Selected, Confirming, Confirmed, Canceled
	}

	private BoardState mState;

	//***************************************************************************
	// Function Name:	Start
	// Purpose:				Sets up the Input Handler.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	void Start ()
	{
		mSelectedHexagon = null;
		mbDoubleTapAvailable = false;
		mbSelectionLocked = false;
		if (mBoard.GetType ().Equals (typeof (HexagonBoard)))
		{
			((HexagonBoard) mBoard).LoadFromFile (Board.mLevelName);
		}
		else
		{
			mBoard.SetUpBoard ();
		}
		mState = BoardState.None;
	}

	//***************************************************************************
	// Function Name:	Update
	// Purpose:				As long as the game is not over, handles input.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	void Update ()
	{
		if (!mGameplayUI.IsGameOver ())
		{
			// Mouse Contols
			HandleMouseInput ();

			// Touch Controls
			HandleTouchInput ();

			mDoubleTapTimer += Time.deltaTime;

			if (mDoubleTapTimer >= DOUBLE_TAP_LENGTH)
			{
				mbDoubleTapAvailable = false;
			}
		}
	}

	//***************************************************************************
	// Function Name:	HandleMouseInput
	// Purpose:				Handles the input from the mouse.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	private void HandleMouseInput ()
	{
		RaycastHit2D hit;
		Hexagon hitHex;
		Vector2 mousePosition;

		mousePosition = mMainCamera.ScreenToWorldPoint (Input.mousePosition);
		hit = Physics2D.Raycast (mousePosition, Vector2.zero, 0f, LayerMask.GetMask (HEXAGON_LAYER));

		switch (mState)
		{
			case BoardState.None:
				if (hit.collider != null)
				{
					hitHex = hit.collider.GetComponent<Hexagon> ();
					mSelectedHexagon = hitHex;
					mState = BoardState.Hover;
				}
				break;
			case BoardState.Hover:
				if (hit.collider != null)
				{
					mBoard.ClearSelections ();
					hitHex = hit.collider.GetComponent<Hexagon> ();
					mSelectedHexagon = hitHex;
					SelectPeg ();
					if (Input.GetMouseButtonDown (0))
					{
						mState = BoardState.Selecting;
					}
				}
				else
				{
					mBoard.ClearSelections ();
					mState = BoardState.None;
				}
				break;
			case BoardState.Selecting:
				HighlightOptions ();
				mState = BoardState.Selected;
				break;
			case BoardState.Selected:
				if (Input.GetMouseButtonDown (0))
				{
					if (hit.collider != null)
					{
						hitHex = hit.collider.GetComponent<Hexagon> ();
						if (hitHex.IsHighlighted ())
						{
							mTargetHexagon = hitHex;
							mState = BoardState.Confirming;
						}
						else
						{
							mState = BoardState.Canceled;
						}
						mBoard.RemoveHighlights ();
					}
					else
					{
						mBoard.RemoveHighlights ();
						mState = BoardState.Canceled;
					}
				}
				break;
			case BoardState.Confirming:
				mState = BoardState.Confirmed;
				break;
			case BoardState.Confirmed:
				MakeMove (mTargetHexagon);
				mState = BoardState.None;
				break;
			case BoardState.Canceled:
				mBoard.ClearSelections ();
				mState = BoardState.None;
				break;
		}
		Debug.Log (mState);
	}

	//***************************************************************************
	// Function Name:	HandleTouchInput
	// Purpose:				Handles touch input.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	private void HandleTouchInput ()
	{
		Hexagon hitHex;
		Vector2 touchPosition;
		RaycastHit2D hit;

		for (int i = 0; i < Input.touchCount; i++)
		{
			touchPosition = mMainCamera.ScreenToWorldPoint (Input.GetTouch (i).position);
			if (Input.GetTouch (i).phase == TouchPhase.Began)
			{
				hit = Physics2D.Raycast (touchPosition, Vector2.zero, 0f, LayerMask.GetMask (HEXAGON_LAYER));

				if (hit.collider != null)
				{
					hitHex = hit.collider.gameObject.GetComponent<Hexagon> ();

					if (mDoubleTapTimer <= DOUBLE_TAP_LENGTH && mbDoubleTapAvailable && !mbSelectionLocked)
					{
						if (hitHex.Equals (mSelectedHexagon))
						{
							HighlightOptions ();
							mbSelectionLocked = true;
						}
					}

					if (mbSelectionLocked && hitHex.IsHighlighted ())
					{
						MakeMove (hitHex);
					}

					if (mSelectedHexagon != null && !hitHex.Equals (mSelectedHexagon) && !mbSelectionLocked)
					{
						mBoard.ClearSelections ();
						mSelectedHexagon = hitHex;
						SelectPeg ();
					}
					else if (mSelectedHexagon == null && !mbSelectionLocked)
					{
						mSelectedHexagon = hitHex;
						SelectPeg ();
					}

					mbDoubleTapAvailable = true;
					mDoubleTapTimer = 0;
				}
				else
				{
					mBoard.ClearSelections ();
					mSelectedHexagon = null;
					mbSelectionLocked = false;
					mBoard.RemoveHighlights ();
				}
			}
		}
	}

	//***************************************************************************
	// Function Name:	MakeMove
	// Purpose:				Finalizes a move.
	// Paramaters:		target - The end target hex of the move.
	// Returns:				None
	//***************************************************************************
	private void MakeMove (Hexagon target)
	{
		int numPegsLeft = 0;
		Jump jump = mBoard.GetJump (mSelectedHexagon, target);
		jump.GetStartHex ().EnablePeg (false);
		jump.GetJumpedHex ().EnablePeg (false);
		jump.GetEndHex ().EnablePeg (true);
		mBoard.ClearSelections ();
		GameDataManager.mSingleton.AddJump (jump);
		mbSelectionLocked = false;
		if (mBoard.IsGameOver (out numPegsLeft))
		{
			if (numPegsLeft > 1)
			{
				// Lose the game
				mGameplayUI.GameOver (false);
			}
			else if (numPegsLeft == 1)
			{
				// Win the game
				mGameplayUI.GameOver (true);
			}

			GameDataManager.mSingleton.WriteGameToFile (GameDataManager.TRIANGLE, Board.mBoardSize);
		}
	}

	//***************************************************************************
	// Function Name:	SelectPeg
	// Purpose:				Attempts to select a peg.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public void SelectPeg ()
	{
		if (mBoard.IsMovePossible (mSelectedHexagon))
		{
			mBoard.RemoveHighlights ();
			mSelectedHexagon.Select (true);
		}
	}

	//***************************************************************************
	// Function Name:	HighlightOptions
	// Purpose:				Highlights all the possible movement options from the
	//								currently selected hex.
	// Paramaters:		None
	// Returns:				None
	//***************************************************************************
	public void HighlightOptions ()
	{
		mBoard.RemoveHighlights ();
		if (mSelectedHexagon.IsPegActive ())
		{
			mBoard.HighlightPossibilities (mSelectedHexagon);
		}
	}
}
