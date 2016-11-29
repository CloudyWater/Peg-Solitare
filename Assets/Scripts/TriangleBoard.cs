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

	public override void HighlightPossibilities (int xPos, int yPos)
	{
		// Up + Left
		if (xPos - 1 >= 0 && yPos - 1 >= 0 && mBoard[xPos - 1, yPos - 1].IsPegActive ())
		{
			if (xPos - 2 >= 0 && yPos - 2 >= 0 && !mBoard[xPos - 2, yPos - 2].IsPegActive ())
			{
				mBoard [xPos - 2, yPos - 2].Highlight (true);
			}
		}

		// Up + Right
		if (yPos - 1 >= xPos && mBoard[xPos, yPos - 1].IsPegActive ())
		{
			if (yPos -2 >= xPos && !mBoard[xPos, yPos - 2].IsPegActive ())
			{
				mBoard [xPos, yPos - 2].Highlight (true);
			}
		}

		// Left
		if (xPos - 1 >= 0 && mBoard [xPos - 1, yPos].IsPegActive ())
		{
			if (xPos - 2 >= 0 && !mBoard [xPos - 2, yPos].IsPegActive ())
			{
				mBoard [xPos - 2, yPos].Highlight (true);
			}
		}

		// Right
		if (xPos + 1 < yPos && mBoard [xPos + 1, yPos].IsPegActive ())
		{
			if (xPos + 2 < yPos && !mBoard[xPos + 2, yPos].IsPegActive ())
			{
				mBoard [xPos + 2, yPos].Highlight (true);
			}
		}

		// Down + Left
		if (yPos + 1 < mBoardSize && mBoard[xPos, yPos + 1].IsPegActive ())
		{
			if (yPos + 2 < mBoardSize && !mBoard[xPos, yPos + 2].IsPegActive ())
			{
				mBoard [xPos, yPos + 2].Highlight (true);
			}
		}

		// Down + Right
		if (yPos + 1 < mBoardSize && xPos + 1 < mBoardSize && mBoard[xPos + 1, yPos + 1].IsPegActive ())
		{
			if (yPos + 2 < mBoardSize && xPos + 2 < mBoardSize && !mBoard[xPos + 2, yPos + 2].IsPegActive ())
			{
				mBoard [xPos + 2, yPos + 2].Highlight (true);
			}
		}
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
}

