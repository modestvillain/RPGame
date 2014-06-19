using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D c)
	{
		GameObject.Destroy(GameObject.Find("Level/Barrier"));

		if(c.gameObject.tag == "Block")
		{
			GameObject.Destroy(c.gameObject);
		}
	}
}
