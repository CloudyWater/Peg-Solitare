using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour
{
	public SpriteRenderer mBase, mHole, mPeg;

	private bool mPegActive;

	// Use this for initialization
	void Start ()
	{

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

	public float GetHexHeight ()
	{
		return mBase.bounds.size.y;
	}

	public float GetHexWidth ()
	{
		return mBase.bounds.size.x;
	}
}
