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

	// Use this for initialization
	void Start ()
	{
		mHole.color = mNormalHoleColor;
	}

	public void Initiate (int xPosition, int yPosition, Board board)
	{
		mXposition = xPosition;
		mYposition = yPosition;
		mBoard = board;
		EnablePeg (true);
		Highlight (false);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void EnablePeg (bool bEnabled)
	{
		mPeg.enabled = bEnabled;
		mbIsPegActive = bEnabled;
	}

	public bool IsPegActive ()
	{
		return mbIsPegActive;
	}

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

	public bool IsHighlighted ()
	{
		return mbIsHighlighted;
	}

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

	public int GetXPosition ()
	{
		return mXposition;
	}

	public int GetYPosition ()
	{
		return mYposition;
	}

	public float GetHexHeight ()
	{
		return mBase.bounds.size.y;
	}

	public float GetHexWidth ()
	{
		return mBase.bounds.size.x;
	}
}
