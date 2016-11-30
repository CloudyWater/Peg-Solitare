using UnityEngine;
using System.Collections;
using System;

public class TriangleBoard : Board
{
	public int mBoardSize;
	public int mVacantPositionX, mVacantPositionY;
	private Hexagon [,] mBoard;

	// Use this for initialization
	protected override void Awake ()
	{
		base.Awake ();
	}

	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	protected override void SetUpBoard ()
	{
		mBoard = new Hexagon [mBoardSize, mBoardSize];
		GameObject rowBase, hexObject;
		Hexagon hexScript;

		for (int i = 0; i < mBoardSize; i++)
		{
			rowBase = GameObject.Instantiate (mHexagonPrefab);
			hexScript = rowBase.GetComponent<Hexagon> ();
			hexScript.Initiate (0, i, this);
			rowBase.transform.position = new Vector2 (-(i / 2.0f) * hexScript.GetHexWidth (),
				-i * hexScript.GetHexHeight () * (.75f));
			rowBase.transform.parent = this.transform;
			rowBase.name = "0," + i.ToString ();
			mBoard [0, i] = hexScript;

			for (int j = 1; j < i + 1; j++)
			{
				hexObject = GameObject.Instantiate (mHexagonPrefab);
				hexScript = hexObject.GetComponent<Hexagon> ();
				hexScript.Initiate (j, i, this);
				hexObject.transform.position = new Vector2 (rowBase.transform.position.x + j * hexScript.GetHexWidth (),
					rowBase.transform.position.y);
				hexObject.transform.parent = this.transform;
				hexObject.name = j.ToString () + "," + i.ToString ();
				mBoard [j, i] = hexScript;
			}
		}

		mBoard [mVacantPositionX, mVacantPositionY].EnablePeg (false);
	}

	public override void ResetBoard ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Select (false);
				hex.Highlight (false);
				if (hex.GetXPosition () == mVacantPositionX && hex.GetYPosition () == mVacantPositionY)
				{
					hex.EnablePeg (false);
				}
				else
				{
					hex.EnablePeg (true);
				}
			}
		}
	}

	public override Jump GetJump (Hexagon selected, Hexagon target)
	{
		Jump retJump = null;
		int jumpedXPosition = selected.GetXPosition ();
		int jumpedYPosition = selected.GetYPosition ();

		if (selected.GetXPosition () > target.GetXPosition ())
		{
			jumpedXPosition = selected.GetXPosition () - 1;
		}
		else if (selected.GetXPosition () < target.GetXPosition ())
		{
			jumpedXPosition = selected.GetXPosition () + 1;
		}

		if (selected.GetYPosition () > target.GetYPosition ())
		{
			jumpedYPosition = selected.GetYPosition () - 1;
		}
		else if (selected.GetYPosition () < target.GetYPosition ())
		{
			jumpedYPosition = selected.GetYPosition () + 1;
		}

		Hexagon jumpedHex = mBoard [jumpedXPosition, jumpedYPosition];

		retJump = new Jump (selected, jumpedHex, target);

		return retJump;
	}

	public override bool IsMovePossible (Hexagon hex)
	{
		bool mbIsPossible = false;
		mbIsPossible = HighlightPossibilities (hex);
		RemoveHighlights ();
		return mbIsPossible;
	}

	public override bool IsGameOver (out int numPegsLeft)
	{
		bool bIsGameOver = true;
		numPegsLeft = 0;
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null && hex.IsPegActive ())
			{
				if (IsMovePossible (hex))
				{
					bIsGameOver = false;
				}
				numPegsLeft++;
			}
		}

		return bIsGameOver;
	}

	public override bool HighlightPossibilities (Hexagon hex)
	{
		bool bMoveIsPossible = false;

		// Up + Left
		if (hex.GetXPosition () - 1 >= 0 && hex.GetYPosition () - 1 >= 0 && mBoard [hex.GetXPosition () - 1,
			hex.GetYPosition () - 1].IsPegActive ())
		{
			if (hex.GetXPosition () - 2 >= 0 && hex.GetYPosition () - 2 >= 0 && !mBoard [hex.GetXPosition () - 2,
				hex.GetYPosition () - 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition () - 2, hex.GetYPosition () - 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Up + Right
		if (hex.GetYPosition () - 1 >= hex.GetXPosition () && mBoard [hex.GetXPosition (),
			hex.GetYPosition () - 1].IsPegActive ())
		{
			if (hex.GetYPosition () - 2 >= hex.GetXPosition () && !mBoard [hex.GetXPosition (),
				hex.GetYPosition () - 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition (), hex.GetYPosition () - 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Left
		if (hex.GetXPosition () - 1 >= 0 && mBoard [hex.GetXPosition () - 1, hex.GetYPosition ()].IsPegActive ())
		{
			if (hex.GetXPosition () - 2 >= 0 && !mBoard [hex.GetXPosition () - 2, hex.GetYPosition ()].IsPegActive ())
			{
				mBoard [hex.GetXPosition () - 2, hex.GetYPosition ()].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Right
		if (hex.GetXPosition () + 1 <= hex.GetYPosition () && mBoard [hex.GetXPosition () + 1,
			hex.GetYPosition ()].IsPegActive ())
		{
			if (hex.GetXPosition () + 2 <= hex.GetYPosition () && !mBoard [hex.GetXPosition () + 2,
				hex.GetYPosition ()].IsPegActive ())
			{
				mBoard [hex.GetXPosition () + 2, hex.GetYPosition ()].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Down + Left
		if (hex.GetYPosition () + 1 < mBoardSize && mBoard [hex.GetXPosition (), hex.GetYPosition () + 1].IsPegActive ())
		{
			if (hex.GetYPosition () + 2 < mBoardSize && !mBoard [hex.GetXPosition (), hex.GetYPosition () + 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition (), hex.GetYPosition () + 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		// Down + Right
		if (hex.GetYPosition () + 1 < mBoardSize && hex.GetXPosition () + 1 < mBoardSize && mBoard [hex.GetXPosition () + 1,
			hex.GetYPosition () + 1].IsPegActive ())
		{
			if (hex.GetYPosition () + 2 < mBoardSize && hex.GetXPosition () + 2 < mBoardSize && !mBoard [hex.GetXPosition () + 2,
				hex.GetYPosition () + 2].IsPegActive ())
			{
				mBoard [hex.GetXPosition () + 2, hex.GetYPosition () + 2].Highlight (true);
				bMoveIsPossible = true;
			}
		}

		return bMoveIsPossible;
	}

	public override void RemoveHighlights ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Highlight (false);
			}
		}
	}

	public override void ClearSelections ()
	{
		foreach (Hexagon hex in mBoard)
		{
			if (hex != null)
			{
				hex.Select (false);
			}
		}
	}

	public Hexagon GetHex (int xPosition, int yPosition)
	{
		return mBoard [xPosition, yPosition];
	}
}

