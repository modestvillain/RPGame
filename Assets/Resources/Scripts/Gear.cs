using UnityEngine;
using System.Collections;

/*
 * Interactive gear that the player can stop/start with
 * freeze/electric spells. Should have an impact on some
 * other component in the environment (e.g. stops a saw
 * from spinning, opens a door, etc.)
 */
public class Gear : MonoBehaviour {

	Animator a;

	void Start()
	{
		a = GetComponent<Animator>();
	}

	/*
	 * Stop the gear, stop related component.
	 */
	public void stop()
	{
		a.SetBool("stopped", true);
		GameObject.Find("Level/Saw").GetComponent<Saw>().stop();
//		GameObject.Find("Saw").GetComponent<Saw>().stop();
	}

	/*
	 * Start gear, start related component.
	 */
	public void start()
	{
		a.SetBool("stopped", false);
		GameObject.Find("Level/Saw").GetComponent<Saw>().start();
//		GameObject.Find("Saw").GetComponent<Saw>().start();
	}
}
