using UnityEngine;
using System.Collections;

public class Health : Item {

	/*
	 * Regenerates player health, signals inventory to be destroyed.
	 */
	public override void useItem()
	{
		GameManager.player.HPRegen(10);
		Inventory.destroyItem(this);
	}
}
