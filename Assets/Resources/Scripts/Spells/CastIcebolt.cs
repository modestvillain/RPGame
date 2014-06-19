using UnityEngine;
using System.Collections;

public class CastIcebolt : Spell {

	void OnEnable()
	{
		cost = GameManager.spellCost("CastIcebolt");
		Player p = GameManager.player;

		if(!p.facingRight)
		{
			transform.position = new Vector2(p.transform.position.x-.5f, p.transform.position.y);
			sign = -1;
			Vector2 v = transform.localScale;
			v.x *= -1;
			transform.localScale = v;
		}
		else
		{
			transform.position = new Vector2(p.transform.position.x+.5f, p.transform.position.y);
			sign = 1;
		}
	}
	
	void Update()
	{
		float newX = sign*Time.deltaTime*speed;
		dist += newX;
		transform.position = new Vector2(transform.position.x + newX, transform.position.y);
		transform.localScale = new Vector2(transform.localScale.x, -1*transform.localScale.y);	/* "FLUTTER" EFFECT TO APPEAR MORE LIVELY */

		if(Mathf.Abs(dist) > maxDist)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Gear")
		{
			c.gameObject.GetComponent<Gear>().stop();
		}
		Destroy(gameObject);
	}
}
