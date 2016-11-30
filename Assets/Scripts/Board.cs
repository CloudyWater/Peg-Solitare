using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Board : MonoBehaviour
{

	public GameObject mHexagonPrefab;

	// Use this for initialization
	protected virtual void Awake ()
	{
		SetUpBoard ();
	}

	// Update is called once per frame
	protected virtual void Update ()
	{

	}

	protected abstract void SetUpBoard ();

	public abstract void ResetBoard ();

	public abstract void ClearSelections ();

	public abstract bool IsMovePossible (Hexagon hex);

	public abstract bool HighlightPossibilities (Hexagon hex);

	public abstract Jump GetJump (Hexagon selected, Hexagon target);

	public abstract bool IsGameOver (out int numPegsLeft);

	public abstract void RemoveHighlights ();
}
