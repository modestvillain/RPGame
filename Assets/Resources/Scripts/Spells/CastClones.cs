using UnityEngine;
using System.Collections;

public class CastClones : Spell {
	
	void Start ()
	{
		cost = 100;
	}

	void Update ()
	{
		if(!GameManager.player.cloned)		/* AN INCORRECTLY CREATED GO HOLDING THIS SCRIPT MUST DESTROY */
			Destroy (gameObject);			/* ITSELF WHEN THE PLAYER INTENDS TO DESTROY CLONES */
	}

	/*
	 * Two possible behaviors:
	 * 		1. Creates 2 Clone GOs on either side of the player
	 * 		   and has them face the correct direction
	 * 		2. Destroys existing clones
	 */
	public override void castSpell()
	{
		Player p = GameManager.player;
		p.cloned = !p.cloned;
		p.livingClones = 2;

		if(p.cloned)
		{
			GameObject clone1 = (GameObject)Instantiate(Resources.Load ("Prefabs/Clone"));
			clone1.transform.position = new Vector2(p.transform.position.x-.5f, p.transform.position.y);
			GameObject clone2 = (GameObject)Instantiate(Resources.Load ("Prefabs/Clone"));
			clone2.transform.position = new Vector2(p.transform.position.x+.5f, p.transform.position.y);
			clone1.transform.localScale = p.transform.localScale;
			clone2.transform.localScale = p.transform.localScale;
			((Clone)clone1.GetComponent("Clone")).facingRight = p.facingRight;
			((Clone)clone2.GetComponent("Clone")).facingRight = p.facingRight;
		}
		else
		{
			GameObject[] clones = GameObject.FindGameObjectsWithTag ("Clone");
			for(int i=0; i<clones.Length; i++)
			{
				((Clone)clones[i].GetComponent("Clone")).killMe();
			}
			Destroy (gameObject);
		}
	}
}
