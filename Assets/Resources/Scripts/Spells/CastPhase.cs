using UnityEngine;
using System.Collections;

public class CastPhase : Spell {

	Player p;

	void OnEnable()
	{
		p = GameManager.player;
	}

	void Update ()
	{
		if(!p.phased)
			Destroy(gameObject);

		// steadily decrease mana???
	}

	/*
	 * Puts the player in "phase" mode, allowing him/her to
	 * interact with special parts of the level.
	 * 
	 * Changes the opacity of the player.
	 */
	public override void castSpell()
	{
		p.phased = !p.phased;

		if(p.phased)
		{
			Color c = p.sr.material.color;
			c.a = .6f;
			p.sr.material.color = c;
		}
		else
		{
			Color c = p.sr.material.color;
			c.a = 1f;
			p.sr.material.color = c;
		}
	}
}
