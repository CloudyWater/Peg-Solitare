using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class LevelEditorHandler : MonoBehaviour
{
	public const string LEVEL_DIRECTORY = "\\Levels\\";
	public const string LEVEL_POSTFIX = ".level";

	public Camera mMainCamera;
	public HexagonBoard mHexagonBoard;
	public Text mNameInput, mSizeInput, mLoadInput;

	public int mBoardSize;
	// Use this for initialization
	void Start ()
	{
		Board.mBoardSize = mBoardSize;
		mHexagonBoard.SetUpBoard ();
	}

	// Update is called once per frame
	void Update ()
	{
		HandleMouseInput ();
	}

	private void HandleMouseInput ()
	{
		RaycastHit2D hit;
		Hexagon hitHex;

		Vector2 mousePosition = mMainCamera.ScreenToWorldPoint (Input.mousePosition);
		hit = Physics2D.Raycast (mousePosition, Vector2.zero, 0f, LayerMask.GetMask (InputHandler.HEXAGON_LAYER));

		if (hit.collider != null)
		{
			hitHex = hit.collider.GetComponent<Hexagon> ();
			if (Input.GetMouseButtonDown (0))
			{
				hitHex.Display (!hitHex.IsDisplayed ());
			}
			if (Input.GetMouseButtonDown (1))
			{
				hitHex.EnablePeg (!hitHex.IsPegActive ());
			}
		}

	}

	public void ResizeLevel ()
	{
		try
		{
			Board.mBoardSize = int.Parse (mSizeInput.text);
			mHexagonBoard.SetUpBoard ();
		}
		catch (System.FormatException)
		{
			Debug.Log ("Ya can't do that man");
		}
	}

	public void SaveLevel ()
	{
		if (mNameInput.text != "")
		{
			Directory.CreateDirectory (Application.persistentDataPath + LEVEL_DIRECTORY);
			mHexagonBoard.WriteToFile (Application.persistentDataPath + LEVEL_DIRECTORY + mNameInput.text + LEVEL_POSTFIX);
		}
	}

	public void LoadLevel ()
	{
		if (mLoadInput.text != "")
		{
			mHexagonBoard.LoadFromFile (Application.persistentDataPath + LEVEL_DIRECTORY + mLoadInput.text + LEVEL_POSTFIX);
		}
	}

	public void ExitEditor ()
	{
		SceneManager.LoadScene (MenuUIHandler.MENU_SCENE);
	}
}
