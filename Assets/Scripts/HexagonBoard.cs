using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class HexagonBoard : Board
{
	private const string LEVEL_SIZE = "Level Size: ";

	private struct BoardData
	{
		public int mBoardSize;
		public int [,] mBoardSpaces;
	}

	private string mLevelFile;

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	public override bool HighlightPossibilities (Hexagon hex)
	{
		bool bMoveIsPossible = false;

		int startX = hex.GetXPosition ();
		int startY = hex.GetYPosition ();
		Hexagon firstSpace, secondSpace;

		// Up + Left
		if (startX - 1 >= 0 && startY - 2 >= 0)
		{
			if (startY % 2 == 0)
			{
				firstSpace = mBoard [startX, startY - 1];
			}
			else
			{
				firstSpace = mBoard [startX - 1, startY - 1];
			}
			secondSpace = mBoard [startX - 1, startY - 2];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		// Up + Right
		if (startX + 1 < mBoardSize && startY - 2 >= 0)
		{
			if (startY % 2 == 0)
			{
				firstSpace = mBoard [startX + 1, startY - 1];
			}
			else
			{
				firstSpace = mBoard [startX, startY - 1];
			}
			secondSpace = mBoard [startX + 1, startY - 2];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		// Left
		if (startX - 2 >= 0)
		{
			firstSpace = mBoard [startX - 1, startY];
			secondSpace = mBoard [startX - 2, startY];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		// Right
		if (startX + 2 < mBoardSize)
		{
			firstSpace = mBoard [startX + 1, startY];
			secondSpace = mBoard [startX + 2, startY];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		// Down + Left
		if (startX - 1 >= 0 && startY + 2 < mBoardSize)
		{
			if (startY % 2 == 0)
			{
				firstSpace = mBoard [startX, startY + 1];
			}
			else
			{
				firstSpace = mBoard [startX - 1, startY + 1];
			}
			secondSpace = mBoard [startX - 1, startY + 2];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		// Down + Right
		if (startX + 1 < mBoardSize && startY + 2 < mBoardSize)
		{
			if (startY % 2 == 0)
			{
				firstSpace = mBoard [startX, startY + 1];
			}
			else
			{
				firstSpace = mBoard [startX - 1, startY + 1];
			}
			secondSpace = mBoard [startX - 1, startY + 2];
			if ((firstSpace != null && firstSpace.IsPegActive ()) && (secondSpace != null && !secondSpace.IsPegActive ()))
			{
				bMoveIsPossible = true;
				secondSpace.Highlight (true);
			}
		}

		return bMoveIsPossible;
	}

	public override void ResetBoard ()
	{
		LoadFromFile (mLevelFile);
	}
	
	public override void SetUpBoard ()
	{
		GameObject hexObject, rowBase;
		bool bIsRowOffset = false;
		Hexagon hexScript;
		if (mBoard != null)
		{
			foreach (Hexagon hex in mBoard)
			{
				if (hex != null)
				{
					Destroy (hex.gameObject);
				}
			}
		}
		mBoard = new Hexagon [mBoardSize, mBoardSize];
		for (int i = 0; i < mBoardSize; i++)
		{
			rowBase = GameObject.Instantiate (mHexagonPrefab);
			hexScript = rowBase.GetComponent<Hexagon> ();
			hexScript.Initiate (0, i, this);
			if (!bIsRowOffset)
			{
				rowBase.transform.position = new Vector2 (-(mBoardSize / 2.0f) * hexScript.GetHexWidth (),
					-i * hexScript.GetHexHeight () * (.75f));
				bIsRowOffset = true;
			}
			else
			{
				rowBase.transform.position = new Vector2 (-(mBoardSize / 2.0f) * hexScript.GetHexWidth () -
					hexScript.GetHexWidth () / 2, -i * hexScript.GetHexHeight () * (.75f));
				bIsRowOffset = false;
			}
			rowBase.transform.parent = this.transform;
			rowBase.name = "0," + i.ToString ();
			mBoard [0, i] = hexScript;

			for (int j = 1; j < mBoardSize; j++)
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
	}

	private void SetUpBoard (BoardData data)
	{
		GameObject hexObject, rowBase;
		bool bIsRowOffset = false;
		Hexagon hexScript;
		if (mBoard != null)
		{
			foreach (Hexagon hex in mBoard)
			{
				if (hex != null)
				{
					Destroy (hex.gameObject);
				}
			}
		}
		mBoard = new Hexagon [data.mBoardSize, data.mBoardSize];
		for (int i = 0; i < data.mBoardSize; i++)
		{
			rowBase = GameObject.Instantiate (mHexagonPrefab);
			hexScript = rowBase.GetComponent<Hexagon> ();
			hexScript.Initiate (0, i, this);
			if (!bIsRowOffset)
			{
				rowBase.transform.position = new Vector2 (-(data.mBoardSize / 2.0f) * hexScript.GetHexWidth (),
					-i * hexScript.GetHexHeight () * (.75f));
				bIsRowOffset = true;
			}
			else
			{
				rowBase.transform.position = new Vector2 (-(data.mBoardSize / 2.0f) * hexScript.GetHexWidth () -
					hexScript.GetHexWidth () / 2, -i * hexScript.GetHexHeight () * (.75f));
				bIsRowOffset = false;
			}
			rowBase.transform.parent = this.transform;
			rowBase.name = "0," + i.ToString ();

			for (int j = 1; j < data.mBoardSize; j++)
			{
				if (data.mBoardSpaces [j, i] > 0)
				{
					hexObject = GameObject.Instantiate (mHexagonPrefab);
					hexScript = hexObject.GetComponent<Hexagon> ();
					hexScript.Initiate (j, i, this);
					hexObject.transform.position = new Vector2 (rowBase.transform.position.x + j * hexScript.GetHexWidth (),
						rowBase.transform.position.y);
					hexObject.transform.parent = this.transform;
					hexObject.name = j.ToString () + "," + i.ToString ();
					mBoard [j, i] = hexScript;

					if (data.mBoardSpaces [j, i] == 2)
					{
						mBoard [j, i].EnablePeg (false);
					}
				}
			}
			if (data.mBoardSpaces [0, i] == 1)
			{
				mBoard [0, i] = hexScript;
			}
			else if (data.mBoardSpaces [0, i] == 2)
			{
				mBoard [0, i] = hexScript;
				mBoard [0, i].EnablePeg (false);
			}
			else
			{
				Destroy (rowBase.gameObject);
			}
		}
	}

	public void WriteToFile (string fileName)
	{
		string dataLine = "";
		BoardData data = new BoardData ();
		data.mBoardSize = Board.mBoardSize;
		data.mBoardSpaces = new int [data.mBoardSize, data.mBoardSize];

		using (StreamWriter writer = File.CreateText (fileName))
		{
			writer.WriteLine (LEVEL_SIZE + data.mBoardSize);
			for (int i = 0; i < data.mBoardSize; i++)
			{
				for (int j = 0; j < data.mBoardSize; j++)
				{
					if (mBoard [i, j] != null && mBoard [i, j].IsDisplayed ())
					{
						if (mBoard [i, j].IsPegActive ())
						{
							data.mBoardSpaces [i, j] = 1;
						}
						else
						{
							data.mBoardSpaces [i, j] = 2;
						}
					}
					else
					{
						data.mBoardSpaces [i, j] = 0;
					}
					dataLine += data.mBoardSpaces [i, j].ToString () + " ";
				}
				writer.WriteLine (dataLine);
				dataLine = "";
			}
			Debug.Log ("File saved to " + fileName);
		}
	}

	public void LoadFromFile (string fileName)
	{
		mLevelFile = fileName;
		BoardData data = new BoardData ();
		string boardSize, boardLine;
		string [] boardLineValues;

		using (StreamReader reader = File.OpenText (fileName))
		{
			boardSize = reader.ReadLine ();
			boardSize = boardSize.Remove (0, LEVEL_SIZE.Length);
			data.mBoardSize = int.Parse (boardSize);
			Board.mBoardSize = data.mBoardSize;
			data.mBoardSpaces = new int [data.mBoardSize, data.mBoardSize];
			for (int i = 0; i < data.mBoardSize; i++)
			{
				boardLine = reader.ReadLine ();
				boardLineValues = boardLine.Split (' ');
				for (int j = 0; j < data.mBoardSize; j++)
				{
					data.mBoardSpaces [i, j] = int.Parse (boardLineValues [j]);
				}
			}

			SetUpBoard (data);
		}
	}

}
