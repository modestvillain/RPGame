using UnityEngine;
using System.Collections;

public class Scroll : Item {

	/*
	 * Signals inventory to be removed, then is placed in front
	 * of the player (the direction the sprite is facing).
	 */
	public override void useItem()
	{
		Inventory.removeItem(this);
		GetComponent<SpriteRenderer>().sortingOrder = 0;
		Vector2 v = transform.localScale;
		v = (v * 4f)/3f;												// restores original size of item
		transform.localScale = v;
		float offset = GameManager.player.facingRight ? .5f : -.5f;		// +/- depending on direction player is facing
		transform.position = new Vector2(GameManager._player.transform.position.x + offset, GameManager._player.transform.position.y);
	}
}