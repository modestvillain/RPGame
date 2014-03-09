using UnityEngine;
using System.Collections;

public static class Inventory {

	static GameObject 	me,
						player,
						screen;
	static int 	index = 0,
				inventorySize=16;
	static GameObject[]	items = new GameObject[inventorySize];
	public static bool	visible=false;
	static Camera cam;

	public static void setup() {
		player = GameObject.Find ("Player");
		cam = GameObject.Find ("MainCamera").camera;
		screen = GameObject.Find ("Inventory");
		screen.SetActive (false);
	}

	public static void addItem(GameObject item) {
		item.SetActive (false);
		item.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		Collider.Destroy (item.collider);
		for(int i=0; i<index ; i++) {
			if(item.name==items[i].name) {
				// TODO; Stack item or w/e
				return;
			}
		}
		items[index] = item;
		index++;
		Vector2 scale = item.transform.localScale;
		scale *= .75f;
		item.transform.localScale = scale;
	}

	public static void destroyItem(GameObject item) {
		// TODO
	}

	public static void toggleDisplay() {
		visible = !visible;
		screen.transform.position = player.transform.position;
		screen.SetActive (visible);
		for(int i=0; i<index ; i++) {
			Vector2 position = screen.transform.position;
			position.x -= 4;
			position.y += 1;
			items[i].transform.position = position;
			items[i].SetActive (visible);
		}
	}
}