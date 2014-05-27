using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool pickedUp = false;

	void Start () {
	
	}

	void Update () {

		if (Input.GetMouseButtonDown(0) && pickedUp) {

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit.collider != null) {
				Debug.Log ("hit");				/* PLACEHOLDER FOR CLICK BEHAVIOR*/
			}
		}
	}
}
