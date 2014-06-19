using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	static GameObject 	_player,
						_black;
	static int 	//index = 0,
				//inventorySize = 16,
				rowMax = 4;
	static float xoff = -.5f,
				 yoff = 1f,
				 scale = 1f;
//	static GameObject[]	items = new GameObject[inventorySize];
	static List<Item> items = new List<Item>();
	public static bool	visible = false;

	void Start()
	{
		Vector2 temp = transform.localScale;
		temp *= scale;
		transform.localScale = temp;
	}

	/*
	 * Allows the screen to initialize some variables
	 * and set itself to inactive.
	 */
	public void setup()
	{
		_player = GameManager._player;
		_black = GameManager._black;
		gameObject.SetActive (false);
	}

	/*
	 * Adds the given item to the player's inventory.
	 */
	public void addItem(GameObject i)
	{
		Item item = i.GetComponent<Item>();
		i.SetActive(false);
		item.pickedUp = true;
		i.GetComponent<SpriteRenderer>().sortingOrder = 1;

//		for(int i=0; i<index ; i++) {
//			if(item.tag==items[i].tag) {
//				// TODO; Stack item or w/e
//				// return;
//			}
//		}

//		items[index] = item;
//		index++;
		items.Add(item);
		Vector2 scale = i.transform.localScale;
		scale *= .75f;
		i.transform.localScale = scale;
	}

	/*
	 * Removes the item from the player's inventory AND
	 * destroys it. See removeItem(Item) for the same
	 * method without the destroying behavior.
	 */
	public static void destroyItem(Item item)
	{
		items.Remove(item);
		Destroy (item.gameObject);
	}

	/*
	 * Removes the item from the player's inventory.
	 */
	public static void removeItem(Item item)
	{
		items.Remove(item);
	}

	/*
	 * Turn the inventory gameObject on/off.
	 */
	public void toggleDisplay()
	{
		visible = !visible;
		gameObject.transform.position = _player.transform.position;
		_black.transform.position = transform.position;
		gameObject.SetActive(visible);
		_black.SetActive(visible);

		if(visible)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;

		int xmod = 0;
		int ymod = 0;

		for(int i=0; i < items.Count; i++)
		{
			xmod = i;
			if(xmod > rowMax)
			{
				xmod = 0;
				ymod++;
			}
			Vector2 position = gameObject.transform.position;
			position.x += xoff + .5f*xmod;
			position.y += yoff - .5f*ymod;
			items[i].gameObject.transform.position = position;
			items[i].gameObject.SetActive(visible);
		}
	}
}