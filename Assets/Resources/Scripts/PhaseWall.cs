using UnityEngine;
using System.Collections;

public class PhaseWall : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player" && GameManager.player.phased)
			GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player" && GameManager.player.phased)
			GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.gameObject.tag == "Player")
			GetComponent<BoxCollider2D>().enabled = true;
	}
}
