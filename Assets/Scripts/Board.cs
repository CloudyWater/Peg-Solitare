using UnityEngine;
using System.Collections;

public abstract class Board : MonoBehaviour
{

	public GameObject mHexagonPrefab;

	// Use this for initialization
	protected virtual void Start ()
	{
		SetUpBoard ();
	}

	// Update is called once per frame
	protected virtual void Update ()
	{

	}

	protected abstract void SetUpBoard ();
}
