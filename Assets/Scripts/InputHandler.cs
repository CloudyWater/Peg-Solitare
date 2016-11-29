using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public const string HEXAGON_LAYER = "Hexagons";

	public Camera mMainCamera;
	public Board mBoard;

	private Hexagon mSelectedHexagon;

	// Use this for initialization
	void Start ()
	{
		mSelectedHexagon = null;
		mBoard.RemoveHighlights ();
	}

	// Update is called once per frame
	void Update ()
	{
		Hexagon hitHex;

		// Mouse Contols
		Vector2 mousePosition = mMainCamera.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (mousePosition, Vector2.zero, 0f, LayerMask.GetMask (HEXAGON_LAYER));
		if (hit)
		{
			hitHex = hit.collider.gameObject.GetComponent<Hexagon> ();
			if (mSelectedHexagon != null && !hitHex.Equals (mSelectedHexagon))
			{
				mSelectedHexagon = hitHex;
				SelectCurrentHexagon ();
			}
			else if (mSelectedHexagon == null)
			{
				mSelectedHexagon = hitHex;
				SelectCurrentHexagon ();
			}
		}
		else
		{
			mBoard.RemoveHighlights ();
			mSelectedHexagon = null;
		}

		// Touch Controls

	}

	public void SelectCurrentHexagon ()
	{
		mBoard.RemoveHighlights ();
		if (mSelectedHexagon.IsPegActive ())
		{
			mBoard.HighlightPossibilities (mSelectedHexagon.GetXPosition (), mSelectedHexagon.GetYPosition ());
		}
	}
}
