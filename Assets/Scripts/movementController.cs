using UnityEngine;
using System.Collections;

public class movementController : MonoBehaviour {
	
	Animator a;
	Collider2D box;
	float speed=2f,
		  maxHeight=.7f,
		  originalY=0f;
	bool facingRight=true,
		 colliding=false,
		 grounded=true,
		 reachedMax=false,
		 jumped=false;

	void Start () {
		a = GetComponent<Animator> ();
		box = GetComponent<BoxCollider2D>();
	}
	
	void FixedUpdate () {

		/* 
		 * Get user input
		 */
		if(Input.GetKeyDown("space") && grounded) {
			grounded=false;
			jumped=true;
			originalY=transform.position.y;
		}
		if (Input.GetKey ("left")) {
			if(facingRight)	{
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else if (Input.GetKey ("right")) {
			if(!facingRight) {
				Flip ();
			}
			a.SetBool("moving",true);
		}
		else a.SetBool("moving",false);

		rigidbody2D.velocity = Move ();
	}

	/*
	 * If collision came from below, reallow jumps
	 * 
	 * "The only problem with it is if for some reason
	 * the initial collision is not below - but you
	 * could probably use OnCollisionStay to solve that"
	 * http://answers.unity3d.com/questions/40588/detect-collision-from-bottom.html
	 */
	void OnCollisionEnter2D(Collision2D c) {
		ContactPoint2D contact = c.contacts[0];
		if(Vector2.Dot(contact.normal, Vector2.up) > 0.5 ||
		   Vector2.Dot(c.contacts[c.contacts.Length-1].normal, Vector2.up) > 0.5)
		{
			Debug.Log("cheese");
			grounded = true;
			reachedMax = false;
			jumped = false;
		}
	}

	void OnCollisionExit2D(Collision2D c) {
		grounded = false;
	}

	/*
	 * Flip sprite from either left->right or right->left
	 */
	void Flip() {
		Vector3 v = gameObject.transform.localScale;
		v.x *= -1;
		gameObject.transform.localScale = v;
		facingRight =! facingRight;
	}

	/*
	 * Calculates the vector to set player velocity to
	 */
	Vector2 Move() {
		Vector2 m = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		m = transform.TransformDirection (m);
		m *= speed;
		if (!grounded && !reachedMax && jumped) {
			if(transform.position.y-originalY>=maxHeight) {
				reachedMax=true;
			}
			else {
				m.y += 50 * speed * Time.deltaTime;
			}
		}
		else {
			m.y -= 10 * 9.8f * Time.deltaTime;
		}
		return m;
	}

	public Vector2 getPosition() {
		return gameObject.transform.position;
	}
}