using UnityEngine;
using System.Collections;

public class TriangleBoard : Board
{
	public int mBoardSize;
	public int mVacantPositionX, mVacantPositionY;
	private Hexagon [,] mBoard;

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

	protected override void SetUpBoard ()
	{
		mBoard = new Hexagon [mBoardSize, mBoardSize];
		GameObject rowBase, hexObject;
		Hexagon hexScript;

		for (int i = 0; i < mBoardSize; i++)
		{
			rowBase = GameObject.Instantiate (mHexagonPrefab);
			hexScript = rowBase.GetComponent<Hexagon> ();
			rowBase.transform.position = new Vector2 (-(i / 2.0f) * hexScript.GetHexWidth (), 
				-i * hexScript.GetHexHeight () * (.75f));
			rowBase.transform.parent = this.transform;
			rowBase.name = "0," + i.ToString ();
			mBoard [0, i] = hexScript;

			for (int j = 1; j < i + 1; j++)
			{
				hexObject = GameObject.Instantiate (mHexagonPrefab);
				hexScript = hexObject.GetComponent<Hexagon> ();
				hexObject.transform.position = new Vector2 (rowBase.transform.position.x + j * hexScript.GetHexWidth (), 
					rowBase.transform.position.y);
				hexObject.transform.parent = this.transform;
				hexObject.name = j.ToString () + "," + i.ToString ();
				mBoard [j, i] = hexScript;
			}
		}

		mBoard [mVacantPositionX, mVacantPositionY].EnablePeg (false);
	}
}
