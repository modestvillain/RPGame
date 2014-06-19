using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	Animator a;
	Collider2D box;
	public SpriteRenderer sr;
	public Inventory inv;
	public Spellbook spb;
	float 	speed = 2f,
		  	accel = 15f,
			jumpAccel = 75f,
			strafeSpeed = 1f,
			normalSpeed = 2f,
			regenTime = 0f;
	public bool 	facingRight = true,
				 	grounded = true,
					reachedMax = false,
					cloned = false,
					phased = false;
	public int		HP = 100,	
					MP = 100,
					maxHP = 100,
					maxMP = 100,
					livingClones = 0;
	public string currentSpell;

	void Start()
	{
		a = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer>();
		currentSpell = "CastFirebolt";						/* DEFAULT SPELL */
	}
	
	void Update()
	{
		if(MP < maxMP)
			regenTime += Time.deltaTime;
		if(regenTime > 1f)
		{
			MPRegen ();
			regenTime = 0f;
		}

		/* SPELL SELECT */
		if(Input.GetKeyDown("1"))
			currentSpell = "CastFirebolt";
		else if(Input.GetKeyDown("2"))
			currentSpell = "CastIcebolt";
		else if(Input.GetKeyDown("3"))
			currentSpell = "CastLightningbolt";
		else if(Input.GetKeyDown("4"))
			currentSpell = "CastClones";
		else if(Input.GetKeyDown("5"))
			currentSpell = "CastPhase";
		RaycastHit2D hitL = Physics2D.Raycast (new Vector2(transform.position.x-.1f,transform.position.y), -Vector2.up, .4f, 1<<8);
		RaycastHit2D hitC = Physics2D.Raycast (transform.position, -Vector2.up, .4f, 1<<8);
		RaycastHit2D hitR = Physics2D.Raycast (new Vector2(transform.position.x+.1f,transform.position.y), -Vector2.up, .4f, 1<<8);

		/* GROUNDED */
		if(hitL.collider != null || hitC.collider != null || hitR.collider != null)
		{
			grounded = true;
			reachedMax = false;
			a.SetBool("jumping",false);
			accel = 10f;
		}
		else
		{
			grounded = false;
		}

		/* INVENTORY */
		if((Input.GetKeyDown("i") || Inventory.visible) && !Spellbook.visible)
		{
			if((Input.GetKeyDown("i") && grounded))
			{
				inv.toggleDisplay();
			}
			return;
		}

		/* SPELLBOOK */
		if((Input.GetKeyDown("s") || Spellbook.visible) && !Inventory.visible)
		{
			if((Input.GetKeyDown("s") && grounded))
			{
				spb.toggleDisplay();
			}
			return;
		}

		/* ACTION BUTTONS */
		if(Input.GetKey("a"))
		{																/* STRAFE */
			speed = strafeSpeed;
		}
		else
		{
			speed = normalSpeed;
		}

		if(Input.GetKeyDown("e"))
		{																/* SWORD ATTACK */
			a.SetBool("attacking",true);

		}
		else if(a.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
		{
			a.SetBool("attacking",false);
		}

		if(Input.GetKeyDown("q"))
		{																/* SPELL CAST */
			if(MP >= GameManager.spellCost(currentSpell))
			{
				a.SetBool("attacking",true);
				GameObject spell = (GameObject)Instantiate(Resources.Load("Prefabs/" + currentSpell));
				Spell spellScript = ((Spell)spell.GetComponent(currentSpell));
				spellScript.castSpell();
				subtractMP(spellScript);
			}
		}

		else if(a.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
		{
			a.SetBool("attacking",false);
		}

		if(Input.GetKeyDown("space") && grounded)
		{																/* JUMP */
			accel = jumpAccel;
			grounded = false;
			a.SetBool("jumping",true);
		}
		else if (Input.GetKey ("left") && !a.GetBool("attacking"))
		{																/* LEFT MOVEMENT */
			if(facingRight && !Input.GetKey ("a"))
			{
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else if (Input.GetKey ("right") && !a.GetBool("attacking"))
		{																/* RIGHT MOVEMENT */
			if(!facingRight && !Input.GetKey ("a"))
			{
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else
		{																/* NO MOVEMENT */
			a.SetBool("moving",false);
		}

		rigidbody2D.velocity = Move ();
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Spikes")
		{																/* TAKE DAMAGE */
			HP -= 10;
			StartCoroutine(blink ());
		}
		else if(c.gameObject.transform.parent != null && c.gameObject.transform.parent.tag == "Item")
		{																/* ADD ITEM */
			inv.addItem(c.gameObject);
		}
	}

	/*
	 * Cause the player sprite to look like it is blinking
	 * on and off (probably due to taking damage).
	 */
	IEnumerator blink()
	{
		for(int i=0; i<5; i++)
		{
			if(i%2==0)	sr.sortingLayerName = "Hidden";
			else        sr.sortingLayerName = "Player";
			yield return new WaitForSeconds(.1f);
		}
		sr.sortingLayerName = "Player";
	}

	/*
	 * Flip sprite from either left->right or right->left.
	 */
	void Flip()
	{
		Vector2 v = gameObject.transform.localScale;
		v.x *= -1;
		gameObject.transform.localScale = v;
		facingRight = !facingRight;
	}

	/*
	 * Controls all player movement.
	 * 
	 * Calculates the vector to set player velocity to,
	 * and monitors jumping status.
	 */
	Vector2 Move()
	{
		Vector2 vector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		if(!a.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			vector = transform.TransformDirection (vector);
			vector *= speed;
		}

		if (!grounded && !reachedMax && a.GetBool("jumping"))
		{
			if(accel < 11f)
			{
				reachedMax=true;
			}
			else if (accel > 15f)
			{
				vector.y += accel * speed * Time.deltaTime;
				accel *= .95f;
			}
			else
			{
				accel *= .95f;
			}
		}
		else
		{
			if(accel < 90f)
				accel *= 1.08f;
			vector.y -= accel * speed * Time.deltaTime;
		}

		return vector;
	}

	/*
	 * Subtracts the appropriate amount of mana
	 * from the player's current MP.
	 */
	void subtractMP(Spell spell)
	{
		MP -= spell.cost;
	}

	/*
	 * Regenerates MP at a specified rate.
	 * 
	 * Called in Update() to regenerate based on
	 * a specified time quantum.
	 */
	void MPRegen()
	{
		MP += 20;
		if(MP >= maxMP)
			MP = maxMP;
	}

	public void HPRegen(int n)
	{
		if(HP >= maxHP)
			HP = maxHP;
		else
			HP += n;
	}

	/*
	 * Resets player attributes/variables upon
	 * respawning.
	 */
	public void respawn()
	{
		HP = maxHP;
		MP = maxMP;
	}

	/*
	 * Called by a dead clone to warn the player
	 * it has lost a good, albeit dumb, soldier.
	 * 
	 * Useful for resetting [cloned] when both clones
	 * die, even if player hasn't called them back.
	 */
	public void cloneDeath()
	{
		livingClones--;

		if(livingClones < 1)
		{
			livingClones = 0;
			cloned = false;
		}
	}
}