using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	
	public int sign;
	public int cost;
	public float 	dist,
					maxDist = 5f,
					maxTime,
					speed = 4f;

	void Start()
	{
		maxDist = 5f;			// for some reason instance variable isn't setting itself, need
								// to do it separately here
	}

	/*
	 * Generic spell casting function that all
	 * child classes must implement.
	 */
	public virtual void castSpell() {}
}