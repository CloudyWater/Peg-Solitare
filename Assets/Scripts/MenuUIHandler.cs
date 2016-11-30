//*****************************************************************************
// File Name:			MenuUIHandler.cs
// File Author:		Thomas Hyman
// Date:					11/30/16
// File Purpose:	Controls the flow of the main menu.
//*****************************************************************************
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuUIHandler : MonoBehaviour
{
	private const string TRIANGLE_SCENE = "PlayTriangle";
	private const string REPLAY_SCENE = "ReplayScene";
	private const int EASY_SIZE = 4;
	private const int MEDIUM_SIZE = 5;
	private const int HARD_SIZE = 6;

	public Toggle mTriangleEasy, mTriangleMedium, mTriangleHard;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void LoadTriangleGame ()
	{
		if (mTriangleEasy.isOn)
		{
			TriangleBoard.mBoardSize = EASY_SIZE;
			TriangleBoard.mVacantPositionX = 0;
			TriangleBoard.mVacantPositionY = 1;
		}
		else if (mTriangleMedium.isOn)
		{
			TriangleBoard.mBoardSize = MEDIUM_SIZE;
			TriangleBoard.mVacantPositionY = TriangleBoard.mVacantPositionX = 0;
		}
		else if (mTriangleHard.isOn)
		{
			TriangleBoard.mBoardSize = HARD_SIZE;
			TriangleBoard.mVacantPositionX = TriangleBoard.mVacantPositionY = 0;
		}

		SceneManager.LoadScene (TRIANGLE_SCENE);
	}

	public void LoadReplay ()
	{
		SceneManager.LoadScene (REPLAY_SCENE);
	}
}
