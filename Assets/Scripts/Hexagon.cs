using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour
{
	public SpriteRenderer mBase, mHole, mPeg;
	public Color mHighlightColor, mNormalColor;

	private Board mBoard;
	private bool mPegActive;
	private int mXposition, mYposition;

	// Use this for initialization
	void Start ()
	{
		mHole.color = mNormalColor;
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
		mPegActive = bEnabled;
	}

	public bool IsPegActive ()
	{
		return mPegActive;
	}

	public void Highlight (bool bIsHighlighted)
	{
		if (bIsHighlighted)
		{
			mHole.color = mHighlightColor;
		}
		else
		{
			mHole.color = mNormalColor;
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
