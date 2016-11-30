//*****************************************************************************
// File Name:			ReplayHandler.cs
// File Author:		Thomas Hyman
// Date:					11/30/16
// File Purpose:	Handles playback of saved games.
//*****************************************************************************
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReplayHandler : MonoBehaviour
{
	public GameObject mBoardObject;
	public Text mRemainingTimeText;

	private Board mBoard;
	// Use this for initialization
	void Awake ()
	{
		GameDataManager.mSingleton.LoadGameFromFile (mBoardObject);
		if (mBoardObject.GetComponent<TriangleBoard> ().enabled)
		{
			mBoard = mBoardObject.GetComponent<TriangleBoard> ();
		}
		else if (mBoardObject.GetComponent<HexagonBoard> ().enabled)
		{
			mBoard = mBoardObject.GetComponent<HexagonBoard> ();
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void AdvanceMove ()
	{
		Jump move = GameDataManager.mSingleton.GetNextMove ();
		if (move != null)
		{
			move.GetStartHex ().EnablePeg (false);
			move.GetJumpedHex ().EnablePeg (false);
			move.GetEndHex ().EnablePeg (true);

			mRemainingTimeText.text = GameplayUIHandler.FormatRemainingTime (move.GetRemainingTime ());
		}
	}

	public void RetractMove ()
	{
		Jump move = GameDataManager.mSingleton.GetPreviousMove ();
		if (move != null)
		{
			move.GetStartHex ().EnablePeg (true);
			move.GetJumpedHex ().EnablePeg (true);
			move.GetEndHex ().EnablePeg (false);

			mRemainingTimeText.text = GameplayUIHandler.FormatRemainingTime (move.GetRemainingTime ());
		}
	}
}
