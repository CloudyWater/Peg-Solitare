using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public const string HEXAGON_LAYER = "Hexagons";
	public const float DOUBLE_TAP_LENGTH = 0.5f;

	public Camera mMainCamera;
	public Board mBoard;
	public GameplayUIHandler mGameplayUI;

	private Hexagon mSelectedHexagon;
	private float mDoubleTapTimer;
	private bool mbDoubleTapAvailable, mbSelectionLocked;

	// Use this for initialization
	void Start ()
	{
		mSelectedHexagon = null;
		mbDoubleTapAvailable = false;
		mbSelectionLocked = false;
		mBoard.RemoveHighlights ();
	}

	// Update is called once per frame
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

	private void HandleMouseInput ()
	{
		RaycastHit2D hit;
		Hexagon hitHex;
		Vector2 mousePosition;

		mousePosition = mMainCamera.ScreenToWorldPoint (Input.mousePosition);
		hit = Physics2D.Raycast (mousePosition, Vector2.zero, 0f, LayerMask.GetMask (HEXAGON_LAYER));

		if (hit.collider != null && !mbSelectionLocked)
		{
			hitHex = hit.collider.gameObject.GetComponent<Hexagon> ();
			if (mSelectedHexagon != null && !hitHex.Equals (mSelectedHexagon))
			{
				mBoard.ClearSelections ();
				mSelectedHexagon = hitHex;
				SelectPeg ();
			}
			else if (mSelectedHexagon == null)
			{
				mSelectedHexagon = hitHex;
				SelectPeg ();
			}
		}
		else if (!mbSelectionLocked)
		{
			if (mSelectedHexagon != null)
			{
				mBoard.ClearSelections ();
			}
			mSelectedHexagon = null;
		}

		if (Input.GetMouseButtonDown (0))
		{
			if (!mbSelectionLocked && mSelectedHexagon != null)
			{
				HighlightOptions ();
				mbSelectionLocked = true;
			}
			else if (mbSelectionLocked && hit.collider != null)
			{
				hitHex = hit.collider.gameObject.GetComponent<Hexagon> ();
				if (hitHex.IsHighlighted ())
				{
					MakeMove (hitHex);
				}
				else
				{
					mBoard.ClearSelections ();
					mbSelectionLocked = false;
				}
			}
			else if (mbSelectionLocked && hit.collider == null)
			{
				mBoard.ClearSelections ();
				mbSelectionLocked = false;
			}
		}
	}

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

	private void MakeMove (Hexagon target)
	{
		int numPegsLeft = 0;
		Jump jump = mBoard.GetJump (mSelectedHexagon, target);
		jump.GetStartHex ().EnablePeg (false);
		jump.GetJumpedHex ().EnablePeg (false);
		jump.GetEndHex ().EnablePeg (true);
		mBoard.ClearSelections ();
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
		}
	}

	public void SelectPeg ()
	{
		if (mBoard.IsMovePossible (mSelectedHexagon))
		{
			mBoard.RemoveHighlights ();
			mSelectedHexagon.Select (true);
		}
	}

	public void HighlightOptions ()
	{
		mBoard.RemoveHighlights ();
		if (mSelectedHexagon.IsPegActive ())
		{
			mBoard.HighlightPossibilities (mSelectedHexagon);
		}
	}
}
