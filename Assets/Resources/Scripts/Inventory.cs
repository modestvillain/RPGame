using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	static GameObject 	player,
						screen;
	static int 	index = 0,
				inventorySize = 16,
				rowMax = 4;
	static float xoff = -.5f,
				 yoff = 1f,
				 scale = 1f;
	static GameObject[]	items = new GameObject[inventorySize];
	public static bool	visible = false;

	void Start() {
		Vector2 temp = transform.localScale;
		temp *= scale;
		transform.localScale = temp;
	}

	public void setup() {

		player = GameManager._player;
		screen = gameObject;
		screen.SetActive (false);
	}

	public void addItem(GameObject item) {

		item.SetActive (false);
		item.GetComponent<Item>().pickedUp = true;
		item.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		//Collider.Destroy (item.collider);
		for(int i=0; i<index ; i++) {
			if(item.tag==items[i].tag) {
				// TODO; Stack item or w/e
				// return;
			}
		}

		items[index] = item;
		index++;
		Vector2 scale = item.transform.localScale;
		scale *= .75f;
		item.transform.localScale = scale;
	}

	public void destroyItem(GameObject item) {
		// TODO
	}

	public void toggleDisplay() {

		visible = !visible;
		screen.transform.position = player.transform.position;
		screen.SetActive (visible);
		int xmod = 0;
		int ymod = 0;

		for(int i=0; i<index ; i++) {
			xmod = i;
			if(xmod > rowMax) {
				xmod = 0;
				ymod++;
			}
			Vector2 position = screen.transform.position;
			position.x += xoff + .5f*xmod;
			position.y += yoff - .5f*ymod;
			items[i].transform.position = position;
			items[i].SetActive (visible);
		}
	}
}