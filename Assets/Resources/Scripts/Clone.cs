using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {

	Animator a;
	Collider2D box;
	SpriteRenderer sr;
	float 	speed = 2f,
			accel = 15f,
			jumpAccel = 75f,
			strafeSpeed = 1f,
			normalSpeed = 2f;
	public bool 	facingRight = true,
					grounded = true,
					reachedMax = false;

	void Start ()
	{
		a = GetComponent<Animator> ();
	}
	
	void Update ()
	{
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

		/* ACTION BUTTONS */
		if(Input.GetKey("a"))
		{																/* STRAFE */
			speed = strafeSpeed;
		}
		else
		{
			speed = normalSpeed;
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
		{
			killMe();
		}
		else if(c.gameObject.transform.parent != null && c.gameObject.transform.parent.tag == "Item")
		{
			GameManager.inventory.addItem(c.gameObject);
		}
	}

	/*
	 * Flip sprite from either left->right or right->left.
	 */
	void Flip()
	{
		Vector2 v = gameObject.transform.localScale;
		v.x *= -1;
		gameObject.transform.localScale = v;
		facingRight =! facingRight;
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
			if(accel < 11f)
				accel *= 1.08f;
			else if(accel < 90f)
				accel *= 1.08f;
			vector.y -= accel * speed * Time.deltaTime;
		}
		
		return vector;
	}

	/*
	 * Used for any script that wants to destroy the clone.
	 * 
	 * Calls cloneDeath() to update Player; useful when all clones
	 * die without being called back, allows [cloned] boolean to be
	 * reset properly
	 */
	public void killMe()
	{
		Destroy(gameObject);
		GameManager.player.cloneDeath();
	}
}